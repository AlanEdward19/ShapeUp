using SocialService.Profile.Common.Enums;

namespace SocialService.Profile.EditProfile;

/// <summary>
/// Comando para editar um perfil
/// </summary>
/// <param name="gender"></param>
/// <param name="birthDate"></param>
/// <param name="bio"></param>
public class EditProfileCommand(
    EGender? gender,
    DateTime? birthDate,
    string? bio)
{
    /// <summary>
    /// Gênero do perfil
    /// </summary>
    public EGender? Gender { get; set; } = gender;
    
    /// <summary>
    /// Data de nascimento do perfil
    /// </summary>
    public DateTime? BirthDate { get; set; } = birthDate;
    
    /// <summary>
    /// Biografia do perfil
    /// </summary>
    public string? Bio { get; set; } = bio;
}