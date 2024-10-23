using MongoDB.Bson.Serialization.Attributes;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends;

public class Friendship
{
    public Friendship(string friendId, DateTime acceptedAt)
    {
        FriendId = friendId;
        AcceptedAt = acceptedAt;
    }

    [BsonElement("friendId")] public string FriendId { get; set; }

    [BsonElement("acceptedAt")] public DateTime? AcceptedAt { get; set; }
}