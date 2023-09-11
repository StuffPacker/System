using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SP.Database.Mongo.Entity;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
}