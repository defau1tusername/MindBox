using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Serilog;
using Serilog.Context;
using Serilog.Extensions.Hosting;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder();
builder.Services.AddMvcCore()
    .AddControllersAsServices();

builder.Services.AddHostedService<PositionsUpdateBgService>();

builder.Services.AddSingleton<IAvitoClient, AvitoClient>();
builder.Services.AddSingleton<AvitoInformationProvider>();

builder.Services.AddSingleton<DiagnosticContext>();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
builder.Services.Configure<AvitoHelperDatabaseSettings>(
    builder.Configuration.GetSection("AvitoHelperDatabase"));
builder.Services.AddSingleton<DBService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "");
builder.Services.AddAuthorization();

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .Filter.ByExcluding(Matching.FromSource("Microsoft"))
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddSingleton((Serilog.ILogger)Log.Logger);

var app = builder.Build();

app.UseSerilogRequestLogging();
app.Use(async (context, next) =>
{
    using (LogContext.PushProperty("RequestId", context.TraceIdentifier))
    {
        Log.Logger.Information($"[Request Method]: {context.Request.Method} [Path]: {context.Request.Path}");
        await next.Invoke();
    }

});

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
        context.Context.Response.Headers.Add("Expires", "-1");
    }
});
app.UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => endpoints.MapControllers());
app.Run();

