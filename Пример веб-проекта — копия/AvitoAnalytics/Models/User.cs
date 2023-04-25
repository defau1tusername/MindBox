using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Security.Cryptography;
using System.Text;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public User()
    {
        UserId = Guid.NewGuid();
        Role = Role.User;
    }
}

public enum Role
{
    Admin,
    User
}