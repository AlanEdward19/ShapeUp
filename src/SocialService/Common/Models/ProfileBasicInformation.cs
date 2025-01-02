namespace SocialService.Common.Models;

/// <summary>
///     ViewModel para informações básicas de um perfil.
/// </summary>
public class ProfileBasicInformation
{
    /// <summary>
    ///     Construtor
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="profileId"></param>
    /// <param name="imageUrl"></param>
    public ProfileBasicInformation(string firstName, string lastName, Guid profileId, string imageUrl)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileId = profileId;
        ImageUrl = imageUrl;
    }

    /// <summary>
    ///     Primeiro nome do perfil.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     Sobrenome do perfil.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public Guid ProfileId { get; set; }
    
    /// <summary>
    /// Url da imagem do perfil.
    /// </summary>
    public string ImageUrl { get; set; }
}