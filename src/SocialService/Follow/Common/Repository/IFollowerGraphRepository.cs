namespace SocialService.Follow.Common.Repository;

/// <summary>
/// Interface do repositório de grafo sobre seguidores.
/// </summary>
public interface IFollowerGraphRepository
{
    /// <summary>
    /// Método para seguir um perfil: cria uma aresta de "FOLLOWING" entre os perfis
    /// </summary>
    /// <param name="followerProfileId"></param>
    /// <param name="followedProfileId"></param>
    /// <returns></returns>
    Task FollowAsync(Guid followerProfileId, Guid followedProfileId);
    
    /// <summary>
    /// Método para deixar de seguir um perfil: deleta a aresta de "FOLLOWING" entre os perfis
    /// </summary>
    /// <param name="followerProfileId"></param>
    /// <param name="followedProfileId"></param>
    /// <returns></returns>
    Task UnfollowAsync(Guid followerProfileId, Guid followedProfileId);
    
    /// <summary>
    /// Método para verificar se um perfil segue outro: retorna a lista de ids de perfil que o perfil dado segue
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<List<string>> GetFollowersAsync(Guid profileId);
    
    /// <summary>
    /// Método para verificar se um perfil é seguido por outro: retorna a lista de ids de perfil que seguem o perfil dado
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<List<string>> GetFollowingAsync(Guid profileId);
}