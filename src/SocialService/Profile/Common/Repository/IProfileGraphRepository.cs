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
    Task DeleteProfileAsync(Guid id);
    
    /// <summary>
    /// Método para obter um perfil do grafo.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Profile> GetProfileAsync(Guid id);
    
    /// <summary>
    /// Método para atualizar um perfil no grafo.
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    Task UpdateProfileAsync(Profile profile);
    
    /// <summary>
    /// Método para obter perfis do grafo.
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<IEnumerable<Profile>> GetProfilesAsync(IEnumerable<Guid> ids);
}