using SocialService.Common.Entities;

namespace SocialService.Friends;

public class FriendRequest : GraphEntity
{
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        SenderProfileId = result["senderProfileId"].ToString();
        ReceiverProfileId = result["receiverProfileId"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        Message = result["message"].ToString();
        
        base.MapToEntityFromNeo4j(result);
    }

    public string SenderProfileId { get; set; } // Profile who sent the request
    public string ReceiverProfileId { get; set; } // Profile who received the request
    public string Message { get; set; } = ""; // Message sent with the request
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}