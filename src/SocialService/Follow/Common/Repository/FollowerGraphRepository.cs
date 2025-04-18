﻿namespace SocialService.Follow.Common.Repository;

/// <summary>
///     Repositório de grafo sobre seguidores.
/// </summary>
/// <param name="graphContext"></param>
public class FollowerGraphRepository(GraphContext graphContext) : IFollowerGraphRepository
{
    /// <summary>
    ///     Método para seguir um perfil: cria uma aresta de "FOLLOWING" entre os perfis
    /// </summary>
    /// <param name="followerProfileId"></param>
    /// <param name="followedProfileId"></param>
    public async Task FollowAsync(string followerProfileId, string followedProfileId)
    {
        var query =
            $"MATCH (a:Profile {{id: '{followerProfileId}'}}), (b:Profile {{id: '{followedProfileId}'}}) CREATE (a)-[:FOLLOWING]->(b)";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para deixar de seguir um perfil: deleta a aresta de "FOLLOWING" entre os perfis
    /// </summary>
    /// <param name="followerProfileId"></param>
    /// <param name="followedProfileId"></param>
    public async Task UnfollowAsync(string followerProfileId, string followedProfileId)
    {
        var query =
            $"MATCH (a:Profile {{id: '{followerProfileId}'}})-[r:FOLLOWING]->(b:Profile {{id: '{followedProfileId}'}}) DELETE r";

        await graphContext.ExecuteQueryAsync(query);
    }

    /// <summary>
    ///     Método para verificar se um perfil segue outro: retorna a lista de ids de perfil que o perfil dado segue
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<List<string>> GetFollowersAsync(string profileId)
    {
        var query = $"MATCH (a:Profile {{id: '{profileId}'}})<-[:FOLLOWING]-(f:Profile) RETURN f.id AS id";

        var result = await graphContext.ExecuteQueryAsync(query);

        return result.Select(record => record["id"].As<string>()).ToList();
    }

    /// <summary>
    ///     Método para verificar se um perfil é seguido por outro: retorna a lista de ids de perfil que seguem o perfil dado
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    public async Task<List<string>> GetFollowingAsync(string profileId)
    {
        var query = $"MATCH (a:Profile {{id: '{profileId}'}})-[:FOLLOWING]->(f:Profile) RETURN f.id AS id";

        var result = await graphContext.ExecuteQueryAsync(query);

        return result.Select(record => record["id"].As<string>()).ToList();
    }
}