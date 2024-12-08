namespace SocialService.Common.Entities;

public abstract class GraphEntity
{
    public Guid Id { get;  set; }
    
    public virtual void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Id = Guid.Parse(result["id"].ToString()!);
    }
}