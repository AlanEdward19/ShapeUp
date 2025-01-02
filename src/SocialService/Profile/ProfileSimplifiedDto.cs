using SocialService.Profile.Common.Enums;

namespace SocialService.Profile;

/// <summary>
///     Objeto de transferência de dados do perfil
/// </summary>
/// <param name="profile"></param>
public class ProfileSimplifiedDto(Profile profile)
{
    /// <summary>
    ///     Id do perfil
    /// </summary>
    public Guid Id { get; private set; } = profile.Id;

    /// <summary>
    ///     Primeiro nome do perfil
    /// </summary>
    public string FirstName { get; private set; } = profile.FirstName;

    /// <summary>
    ///     Sobrenome do perfil
    /// </summary>
    public string LastName { get; private set; } = profile.LastName;
    
    /// <summary>
    /// Url da imagem do perfil.
    /// </summary>
    public string ImageUrl { get; private set; } = profile.ImageUrl;
    
    /// <summary>
    /// Método para setar a url da imagem do perfil.
    /// </summary>
    /// <param name="imageUrl"></param>
    public void SetImageUrl(string imageUrl) => ImageUrl = imageUrl;
}