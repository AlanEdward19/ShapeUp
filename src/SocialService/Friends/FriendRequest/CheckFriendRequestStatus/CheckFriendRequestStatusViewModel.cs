using SocialService.Friends.Common.Enums;

namespace SocialService.Friends.FriendRequest.CheckFriendRequestStatus;

/// <summary>
///     ViewModel para a verificação do status de uma solicitação de amizade.
/// </summary>
public class CheckFriendRequestStatusViewModel
{
    /// <summary>
    ///     Construtor.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="status"></param>
    /// <param name="profileId"></param>
    public CheckFriendRequestStatusViewModel(string firstName, string lastName, EFriendStatus status, string profileId)
    {
        FirstName = firstName;
        LastName = lastName;
        Status = status;
        ProfileId = profileId;
    }

    /// <summary>
    ///     Primeiro nome do perfil.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    ///     Sobrenome do perfil.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    ///     Status da solicitação de amizade.
    /// </summary>
    public EFriendStatus Status { get; private set; }

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public string ProfileId { get; private set; }
}