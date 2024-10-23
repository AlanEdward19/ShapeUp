using SocialService.Profile.Common.Enums;

namespace SocialService.Profile.CreateProfile;

/// <summary>
/// Comando para criar um perfil
/// </summary>
/// <param name="id"></param>
/// <param name="email"></param>
/// <param name="gender"></param>
/// <param name="birthDate"></param>
/// <param name="bio"></param>
public class CreateProfileCommand(
    string email,
    EGender gender,
    DateTime birthDate,
    string? bio)
{
    /// <summary>
    /// Email do perfil
    /// </summary>
    public string Email { get; set; } = email;

    /// <summary>
    /// Gênero do perfil
    /// </summary>
    public EGender Gender { get; set; } = gender;

    /// <summary>
    /// Data de nascimento do perfil
    /// </summary>
    public DateTime BirthDate { get; set; } = birthDate;

    /// <summary>
    /// Biografia do perfil
    /// </summary>
    public string? Bio { get; set; } = bio;

    /// <summary>
    /// Data de criação do perfil
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
}