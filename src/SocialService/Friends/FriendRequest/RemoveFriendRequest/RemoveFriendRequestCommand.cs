namespace SocialService.Friends.FriendRequest.RemoveFriendRequest;

/// <summary>
///     Comando para remover uma solicitação de amizade.
/// </summary>
public class RemoveFriendRequestCommand
{
    /// <summary>
    ///     Construtor.
    /// </summary>
    /// <param name="profileId"></param>
    public RemoveFriendRequestCommand(string profileId)
    {
        ProfileId = profileId;
    }

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public string ProfileId { get; private set; }
}