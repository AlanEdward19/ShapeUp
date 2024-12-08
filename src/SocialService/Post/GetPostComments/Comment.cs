using SocialService.Common.Entities;

namespace SocialService.Post.GetPostComments;

public class Comment : GraphEntity
{
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        ProfileId = Guid.Parse(result["profileId"].ToString());
        Content = result["content"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        
        base.MapToEntityFromNeo4j(result);
    }
    
    public Guid ProfileId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Content { get; private set; }
}