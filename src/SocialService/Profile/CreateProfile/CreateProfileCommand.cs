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
/// <param name="createdAt"></param>
public class CreateProfileCommand(
    string email,
    EGender gender,
    DateTime birthDate,
    string? bio,
    DateTime createdAt)
{
    public string Email { get; set; } = email;
    public EGender Gender { get; set; } = gender;
    public DateTime BirthDate { get; set; } = birthDate;
    public string? Bio { get; set; } = bio;
    public DateTime CreatedAt { get; set; } = createdAt;
}