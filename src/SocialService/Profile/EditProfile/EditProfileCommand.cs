using SocialService.Profile.Common.Enums;

namespace SocialService.Profile.EditProfile;

/// <summary>
/// Comando para criar um perfil
/// </summary>
/// <param name="id"></param>
/// <param name="email"></param>
/// <param name="gender"></param>
/// <param name="birthDate"></param>
/// <param name="bio"></param>
public class EditProfileCommand(
    string email,
    EGender gender,
    DateTime birthDate,
    string? bio)
{
    public string Email { get; set; } = email;
    public EGender Gender { get; set; } = gender;
    public DateTime BirthDate { get; set; } = birthDate;
    public string? Bio { get; set; } = bio;
}