using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DBService
{
    private readonly IMongoCollection<User> userCollection;
    private readonly IMongoCollection<Ad> adsCollection;
    private readonly IMongoCollection<PositionStatistics> positionStatisticsCollection;
    
    public DBService(
        IOptions<AvitoHelperDatabaseSettings> avitoHelperDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            avitoHelperDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            avitoHelperDatabaseSettings.Value.DatabaseName);

        adsCollection = mongoDatabase.GetCollection<Ad>(
            avitoHelperDatabaseSettings.Value.AdsCollectionName);

        positionStatisticsCollection = mongoDatabase.GetCollection<PositionStatistics>(
            avitoHelperDatabaseSettings.Value.PositionStatisticsCollectionName);

        userCollection = mongoDatabase.GetCollection<User>(
            avitoHelperDatabaseSettings.Value.UserCollectionName);
    }

    #region UserCollectionMethods
    public async Task<User> CheckUserNameAvailabilityAsync(string name) =>
        await userCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

    public async Task AddUserAsync(User user) =>
        await userCollection.InsertOneAsync(user);

    public async Task<User> GetUserAsync(string name, string password) =>
        await userCollection.Find(x => x.Name == name && x.Password == password).FirstOrDefaultAsync();

    public async Task<List<User>> GetAllUsersAsync() =>
        await userCollection.Find(_=> true).ToListAsync();
    #endregion

    #region AdsCollectionMethods
    public async Task<List<Ad>> GetAllAdsAsync(Guid userId) =>
        await adsCollection.Find(x => x.UserId == userId).ToListAsync();

    public async Task<int> NumberOfAdsPerUser(Guid userId) =>
        unchecked((int) await adsCollection.CountDocumentsAsync(ad => ad.UserId == userId));

    public async Task<Ad> GetAdAsync(Guid postId) =>
        await adsCollection.Find(x => x.PostID == postId).FirstOrDefaultAsync();

    public async Task<string> GetAdNameAsync(Guid postID)
    {
        var ad = await adsCollection.Find(x => x.PostID == postID).FirstOrDefaultAsync();
        return ad?.Name;
    }

    public async IAsyncEnumerable<Ad> ToAsyncEnumerable()
    {
        using var cursorSource = await adsCollection.FindAsync("{}");
        while (await cursorSource.MoveNextAsync())
            foreach (var current in cursorSource.Current)
                yield return current;
    }

    public async Task RemoveAdAsync(Guid postID) =>
        await adsCollection.DeleteOneAsync(x => x.PostID == postID);
    public async Task RemovePositionStatisticsAsync(Guid postID) =>
        await positionStatisticsCollection.DeleteManyAsync(x => x.PostID == postID);

    public async Task CreateAdAsync(Ad newAd) =>
        await adsCollection.InsertOneAsync(newAd);

    public async Task UpdateCurrentPositionAsync(Guid postID, int newPosition)
    {
        var filter = Builders<Ad>.Filter.Eq("PostID", postID);
        var updateSetting = new BsonDocument("$set", new BsonDocument("Position", newPosition));
        await adsCollection.UpdateOneAsync(filter, updateSetting);
    }
    #endregion

    #region PositionStatisticsCollectionMethods
    public async Task<List<PositionStatistics>> GetAllPositionsAsync(Guid postID) =>
        await positionStatisticsCollection.Find(x => x.PostID == postID).ToListAsync();

    public async Task AddNewPositionToStatistics(Guid postID, int newPosition)
    {
        var filter = Builders<PositionStatistics>.Filter.Eq("PostID", postID) 
            & Builders<PositionStatistics>.Filter.Eq("Date", DateTime.Today);
        var updateSettingPosition = Builders<PositionStatistics>.Update.Push("Positions", newPosition);
        var updateSettingUpdateTime = Builders<PositionStatistics>.Update.Push("UpdateHour", DateTime.Now.Hour);
        var updateSettings = Builders<PositionStatistics>.Update.Combine(updateSettingPosition, updateSettingUpdateTime);
        var options = new UpdateOptions { IsUpsert = true };

        await positionStatisticsCollection.UpdateOneAsync(filter, updateSettings, options);  
    }
    #endregion

}

