namespace SocialService.Friends.RemoveFriendRequest;

/// <summary>
///     Comando para remover uma solicitação de amizade.
/// </summary>
public class RemoveFriendRequestCommand
{
    /// <summary>
    ///     Construtor.
    /// </summary>
    /// <param name="profileId"></param>
    public RemoveFriendRequestCommand(Guid profileId)
    {
        ProfileId = profileId;
    }

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; }
}