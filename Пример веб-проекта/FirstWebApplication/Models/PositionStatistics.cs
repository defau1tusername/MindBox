using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

public class PositionStatistics
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Guid PostID { get; set; }
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime Date { get; set; }
    public int[] UpdateHour { get; set; }
    public int[] Positions { get; set; }
}

