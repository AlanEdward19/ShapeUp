using SocialService.Profile.Common.Enums;

namespace SocialService.Profile;

/// <summary>
/// Agregador de perfil.
/// </summary>
/// <param name="profile"></param>
public class ProfileAggregate(Profile profile)
{
    /// <summary>
    /// Id do perfil
    /// </summary>
    public Guid ObjectId { get; private set; } = profile.ObjectId;
    
    /// <summary>
    /// Email do perfil
    /// </summary>
    public string Email { get; private set; } = profile.Email;
    
    /// <summary>
    /// Primeiro nome do perfil
    /// </summary>
    public string FirstName { get; private set; } = profile.FirstName;
    
    /// <summary>
    /// Sobrenome do perfil
    /// </summary>
    public string LastName { get; private set; } = profile.LastName;
    
    /// <summary>
    /// Url da imagem do perfil no blob storage
    /// </summary>
    public string? ImageUrl { get; private set; } = profile.ImageUrl;
    
    /// <summary>
    /// Biografia do perfil
    /// </summary>
    public string? Bio { get; private set; } = profile.Bio;
    
    /// <summary>
    /// Data de nascimento do perfil
    /// </summary>
    public DateTime BirthDate { get; private set; } = profile.BirthDate;
    
    /// <summary>
    /// Gênero do perfil
    /// </summary>
    public EGender Gender { get; private set; } = profile.Gender;

    /// <summary>
    /// Método para atualizar a imagem do perfil.
    /// </summary>
    /// <param name="imageUrl"></param>
    public void UpdateImage(string? imageUrl)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl) &&
            (ImageUrl == null || !ImageUrl.Equals(imageUrl, StringComparison.InvariantCultureIgnoreCase)))
            ImageUrl = imageUrl;
    }

    /// <summary>
    /// Método para atualizar a biografia do perfil.
    /// </summary>
    /// <param name="bio"></param>
    public void UpdateBio(string? bio)
    {
        if (!string.IsNullOrWhiteSpace(bio) &&
            (Bio == null || !Bio.Equals(bio, StringComparison.InvariantCultureIgnoreCase)))
            Bio = bio;
    }

    /// <summary>
    /// Método para atualizar o gênero do perfil.
    /// </summary>
    /// <param name="gender"></param>
    public void UpdateGender(EGender? gender)
    {
        if (gender != null)
            Gender = gender.Value;
    }

    /// <summary>
    /// Método para atualizar a data de nascimento do perfil.
    /// </summary>
    /// <param name="birthDate"></param>
    public void UpdateBirthDate(DateTime? birthDate)
    {
        if (birthDate != null)
            BirthDate = birthDate.Value;
    }
}