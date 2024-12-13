using SocialService.Profile.Common.Enums;

namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Comando para criar um perfil
/// </summary>
/// <param name="gender"></param>
/// <param name="birthDate"></param>
/// <param name="bio"></param>
public class CreateProfileCommand(
    EGender gender,
    DateTime birthDate,
    string? bio)
{
    /// <summary>
    ///     Email do perfil
    /// </summary>
    public string Email { get; private set; } = "";

    /// <summary>
    ///     Primeiro nome do perfil
    /// </summary>
    public string FirstName { get; private set; } = "";

    /// <summary>
    ///     Sobrenome do perfil
    /// </summary>
    public string LastName { get; private set; } = "";
    
    /// <summary>
    /// Cidade do perfil
    /// </summary>
    public string City { get; private set; } = "";
    
    /// <summary>
    /// Estado do perfil
    /// </summary>
    public string State { get; private set; } = "";
    
    /// <summary>
    /// País do perfil
    /// </summary>
    public string Country { get; private set; } = "";

    /// <summary>
    ///     Gênero do perfil
    /// </summary>
    public EGender Gender { get; set; } = gender;

    /// <summary>
    ///     Data de nascimento do perfil
    /// </summary>
    public DateTime BirthDate { get; set; } = birthDate;

    /// <summary>
    ///     Biografia do perfil
    /// </summary>
    public string? Bio { get; set; } = bio;

    /// <summary>
    ///     Data de criação do perfil
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    /// <summary>
    ///     Método para set o primeiro nome do perfil
    /// </summary>
    /// <param name="firstName"></param>
    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }

    /// <summary>
    ///     Método para set o sobrenome do perfil
    /// </summary>
    /// <param name="lastName"></param>
    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    /// <summary>
    ///     Método para set o email do perfil
    /// </summary>
    /// <param name="email"></param>
    public void SetEmail(string email)
    {
        Email = email;
    }
    
    /// <summary>
    /// Método para setar a cidade do perfil
    /// </summary>
    /// <param name="city"></param>
    public void SetCity(string city)
    {
        City = city;
    }
    
    /// <summary>
    /// Método para setar o estado do perfil
    /// </summary>
    /// <param name="state"></param>
    public void SetState(string state)
    {
        State = state;
    }
    
    /// <summary>
    /// Método para setar o país do perfil
    /// </summary>
    /// <param name="country"></param>
    public void SetCountry(string country)
    {
        Country = country;
    }
}