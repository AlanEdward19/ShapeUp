namespace SocialService.Profile.Common.Repository;

/// <summary>
///     Interface para repositório de grafo sobre perfis.
/// </summary>
public interface IProfileGraphRepository
{
    /// <summary>
    ///     Método para criar um perfil no grafo.
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    Task CreateProfileAsync(Profile profile);

    /// <summary>
    ///     Método para deletar um perfil do grafo.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteProfileAsync(string id);

    /// <summary>
    ///     Método para obter um perfil do grafo.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requesterId"></param>
    /// <returns></returns>
    Task<Profile?> GetProfileAsync(string id, string? requesterId = null);

    /// <summary>
    ///     Método para atualizar um perfil no grafo.
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    Task UpdateProfileAsync(Profile profile);

    /// <summary>
    ///     Método para obter perfis do grafo.
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IEnumerable<Profile>> GetProfilesAsync(IEnumerable<string> ids);

    /// <summary>
    ///     Método para verificar se um perfil existe.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> ProfileExistsAsync(string id);
}