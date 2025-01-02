namespace SocialService.Profile.GetProfilePictures;

/// <summary>
///     Classe para representar uma foto de perfil.
/// </summary>
public class ProfilePicture(string imageUrl, DateTime createdAt)
{
    /// <summary>
    ///     Caminho da imagem.
    /// </summary>
    public string ImageUrl { get; private set; } = imageUrl;

    /// <summary>
    ///     Data de criação.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = createdAt;
}