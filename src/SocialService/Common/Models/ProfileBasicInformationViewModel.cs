namespace SocialService.Common.Models;

/// <summary>
/// ViewModel para informações básicas de um perfil.
/// </summary>
public class ProfileBasicInformationViewModel
{
    /// <summary>
    /// Primeiro nome do perfil.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Sobrenome do perfil.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Id do perfil.
    /// </summary>
    public Guid ProfileId { get; set; }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="profileId"></param>
    public ProfileBasicInformationViewModel(string firstName, string lastName, Guid profileId)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileId = profileId;
    }
}