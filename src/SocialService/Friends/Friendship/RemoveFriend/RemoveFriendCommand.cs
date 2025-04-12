namespace SocialService.Friends.Friendship.RemoveFriend;

/// <summary>
///     Comando para remover um amigo.
/// </summary>
public class RemoveFriendCommand
{
    /// <summary>
    ///     Construtor.
    /// </summary>
    /// <param name="profileId"></param>
    public RemoveFriendCommand(string profileId)
    {
        ProfileId = profileId;
    }

    /// <summary>
    ///     Id do perfil.
    /// </summary>
    public string ProfileId { get; private set; }
}