using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


public class Ad
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PostID { get; set; }
    public string AdID { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string KeyWords { get; set; }
    public string City { get; set; }
    public int Position { get; set; }

    public Ad() => PostID = Guid.NewGuid();
}



