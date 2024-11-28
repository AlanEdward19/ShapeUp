using SocialService.Database.Graph;

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
            DETACH DELETE p";
        
        await graphContext.ExecuteQueryAsync(query);
    }
}