using SocialService.Common.Entities;

namespace SocialService.Friends;

public class FriendRequest : GraphEntity
{
    public FriendRequest(Guid id) : base(id, "FriendRequest")
    {
    }

    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Id = result["id"].ToString();
        SenderProfileId = result["senderProfileId"].ToString();
        ReceiverProfileId = result["receiverProfileId"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        Message = result["message"].ToString();
    }

    public string SenderProfileId { get; set; } // Profile who sent the request
    public string ReceiverProfileId { get; set; } // Profile who received the request
    public string Message { get; set; } = ""; // Message sent with the request
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}