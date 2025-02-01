using SocialService.Profile.Common.Enums;

namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Comando para criar um perfil
/// </summary>
public class CreateProfileCommand
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
    ///    País do perfil
    /// </summary>
    public string Country { get; private set; } = "";
    
    /// <summary>
    ///    CEP do perfil
    /// </summary>
    public string PostalCode { get; private set; } = "";

    /// <summary>
    ///    Nome de exibição do perfil
    /// </summary>
    public string DisplayName { get; private set; } = "";

    /// <summary>
    ///     Gênero do perfil
    /// </summary>
    public EGender? Gender { get; private set; } = null;

    /// <summary>
    ///     Data de nascimento do perfil
    /// </summary>
    public DateTime? BirthDate { get; private set; } = null;

    /// <summary>
    ///     Biografia do perfil
    /// </summary>
    public string? Bio { get; private set; } = "";

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
    ///     Método para setar o CEP do perfil
    /// </summary>
    /// <param name="postalCode"></param>
    public void SetPostalCode(string postalCode)
    {
        PostalCode = postalCode;
    }
    
    /// <summary>
    /// Método para setar o nome de exibição do perfil
    /// </summary>
    /// <param name="displayName"></param>
    public void SetDisplayName(string displayName)
    {
        DisplayName = displayName;
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