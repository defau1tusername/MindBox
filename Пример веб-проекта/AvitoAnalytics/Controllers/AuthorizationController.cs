using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace FirstWebApplication.Controllers
{
    public class AuthorizationController : ControllerBase
    {
        private readonly DBService dbService;

        public AuthorizationController(DBService db) => dbService = db;
        
        [Route("")]
        [HttpGet]
        public IActionResult GetLoginPage() => 
            Content(System.IO.File.ReadAllText("./html/index.html"), "text/html");

        [Route("/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInForm signInForm)
        {
            if (signInForm.Name == string.Empty || signInForm.Password == string.Empty)
                return BadRequest("Email и/или пароль не установлены");

            var user = await dbService.GetUserAsync(signInForm.Name, PasswordService.HashPassword(signInForm.Password));
            if (user == null) return Unauthorized();

            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Ok();
        }

        [Route("/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignInForm signInForm)
        {
            if (signInForm.Name == string.Empty || signInForm.Password == string.Empty)
                return BadRequest("Email и/или пароль не установлены");

            if (await dbService.CheckUserNameAvailabilityAsync(signInForm.Name) != null) return Unauthorized();

            var user = new User() { Name = signInForm.Name, Password = PasswordService.HashPassword(signInForm.Password) };
            await dbService.AddUserAsync(user);

            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Ok();
        }

        [Route("/logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
