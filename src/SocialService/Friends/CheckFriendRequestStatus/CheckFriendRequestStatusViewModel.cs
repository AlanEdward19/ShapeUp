using SocialService.Friends.Common.Enums;

namespace SocialService.Friends.CheckFriendRequestStatus;

/// <summary>
/// ViewModel para a verificação do status de uma solicitação de amizade.
/// </summary>
public class CheckFriendRequestStatusViewModel
{
    /// <summary>
    /// Primeiro nome do perfil.
    /// </summary>
    public string FirstName { get; private set; }
    
    /// <summary>
    /// Sobrenome do perfil.
    /// </summary>
    public string LastName { get; private set; }
    
    /// <summary>
    /// Status da solicitação de amizade.
    /// </summary>
    public EFriendStatus Status { get; private set; }
    
    /// <summary>
    /// Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; }

    /// <summary>
    /// Construtor.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="status"></param>
    /// <param name="profileId"></param>
    public CheckFriendRequestStatusViewModel(string firstName, string lastName, EFriendStatus status, Guid profileId)
    {
        FirstName = firstName;
        LastName = lastName;
        Status = status;
        ProfileId = profileId;
    }
}