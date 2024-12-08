using SocialService.Common.Entities;

namespace SocialService.Post.GetReactionsOnPost;

public class Reaction : GraphEntity
{
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        ProfileId = Guid.Parse(result["profileId"].ToString());
        ReactionType = result["type"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        
        base.MapToEntityFromNeo4j(result);
    }
    
    public Guid ProfileId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string ReactionType { get; private set; }
}