namespace SocialService.Common.Entities;

public abstract class GraphEntity
{
    public string Id { get;  set; }
    public string Label { get; private set; }
    
    public GraphEntity(Guid id, string label)
    {
        Id = id.ToString();
        Label = label;
    }

    public GraphEntity(string label)
    {
        Id = Guid.NewGuid().ToString();
        Label = label;
    }
    
    public virtual void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Id = (string)result["id"];
    }
}