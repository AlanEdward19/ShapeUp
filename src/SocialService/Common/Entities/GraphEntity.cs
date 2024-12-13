namespace SocialService.Common.Entities;

/// <summary>
/// Entidade base para entidades do grafo.
/// </summary>
public abstract class GraphEntity
{
    /// <summary>
    /// Id da entidade
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Método para mapear os dados do neo4j para a entidade
    /// </summary>
    /// <param name="result"></param>
    public virtual void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Id = Guid.Parse(result["id"].ToString()!);
    }
}