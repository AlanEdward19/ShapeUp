namespace SocialService.Profile.Common.Repository;

/// <summary>
///     Interface para repositório de grafo sobre perfis.
/// </summary>
public interface IProfileGraphRepository
{
    /// <summary>
    ///     Método para criar um perfil no grafo.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task CreateProfileAsync(Guid id);

    /// <summary>
    ///     Método para deletar um perfil do grafo.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteProfileAsync(Guid id);
}