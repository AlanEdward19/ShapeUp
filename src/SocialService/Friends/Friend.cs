using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialService.Friends;

public class Friend
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("profileId")]
    public Guid ProfileId { get; set; }
    
    [BsonElement("friends")]
    public List<Friendship> Friends { get; set; } = new();
}