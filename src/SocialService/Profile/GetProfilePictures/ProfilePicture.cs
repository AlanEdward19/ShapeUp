namespace SocialService.Profile.GetProfilePictures;

/// <summary>
///     Classe para representar uma foto de perfil.
/// </summary>
public class ProfilePicture(string imagePath, DateTime createdAt)
{
    /// <summary>
    ///     Caminho da imagem.
    /// </summary>
    public string ImagePath { get; private set; } = imagePath;

    /// <summary>
    ///     Data de criação.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = createdAt;
}