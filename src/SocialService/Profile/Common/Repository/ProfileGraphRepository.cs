using SocialService.Database.Graph;
using SocialService.Storage;

namespace SocialService.Profile.Common.Repository;

/// <summary>
/// Repositório de grafo sobre perfis.
/// </summary>
/// <param name="graphContext"></param>
public class ProfileGraphRepository(GraphContext graphContext) : IProfileGraphRepository
{
    /// <summary>
    /// Método para criar um perfil
    /// </summary>
    /// <param name="id"></param>
    public async Task CreateProfileAsync(Guid id)
    {
        var query = $@"
            CREATE (p:Profile {{id: '{id}'}})";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    /// Método para deletar um perfil
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteProfileAsync(Guid id)
    {
        var query = $@"
    MATCH (p:Profile {{id: '{id}'}})
    OPTIONAL MATCH (p)-[:PUBLISHED_BY]->(post:Post)
    OPTIONAL MATCH (p)-[:REACTED]->(post)
    OPTIONAL MATCH (p)-[:FRIEND]->(friend:Profile)
    OPTIONAL MATCH (p)-[:SENT_REQUEST]->(request:FriendRequest)
    OPTIONAL MATCH (p)<-[:RECEIVED_REQUEST]-(request)
    OPTIONAL MATCH (p)-[:FOLLOWING]->(followed:Profile)
    OPTIONAL MATCH (p)<-[:FOLLOWING]-(follower:Profile)
    OPTIONAL MATCH (post)<-[:COMMENTED_ON]-(comment:Comment)
    OPTIONAL MATCH (post)<-[r:REACTED]-()
    DETACH DELETE p, post, comment, r, friend, request, followed, follower";

        await graphContext.ExecuteQueryAsync(query);
    }
}