namespace SocialService.Follow.Common.Repository;

/// <summary>
///     Interface do repositório de grafo sobre seguidores.
/// </summary>
public interface IFollowerGraphRepository
{
    /// <summary>
    ///     Método para seguir um perfil: cria uma aresta de "FOLLOWING" entre os perfis
    /// </summary>
    /// <param name="followerProfileId"></param>
    /// <param name="followedProfileId"></param>
    /// <returns></returns>
    Task FollowAsync(string followerProfileId, string followedProfileId);

    /// <summary>
    ///     Método para deixar de seguir um perfil: deleta a aresta de "FOLLOWING" entre os perfis
    /// </summary>
    /// <param name="followerProfileId"></param>
    /// <param name="followedProfileId"></param>
    /// <returns></returns>
    Task UnfollowAsync(string followerProfileId, string followedProfileId);

    /// <summary>
    ///     Método para verificar se um perfil segue outro: retorna a lista de ids de perfil que o perfil dado segue
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<List<string>> GetFollowersAsync(string profileId);

    /// <summary>
    ///     Método para verificar se um perfil é seguido por outro: retorna a lista de ids de perfil que seguem o perfil dado
    /// </summary>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<List<string>> GetFollowingAsync(string profileId);
}