namespace SocialService.Friends.RemoveFriend;

/// <summary>
///     Comando para remover um amigo.
/// </summary>
public class RemoveFriendCommand
{
    /// <summary>
    ///     Construtor.
    /// </summary>
    /// <param name="profileId"></param>
    public RemoveFriendCommand(Guid profileId)
    {
        ProfileId = profileId;
    }

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public Guid ProfileId { get; private set; }
}