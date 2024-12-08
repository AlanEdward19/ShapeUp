namespace SocialService.Follow.FollowUser;

/// <summary>
///     Comando para seguir um usuário.
/// </summary>
public class FollowUserCommand
{
    /// <summary>
    ///     Construtor do comando.
    /// </summary>
    /// <param name="followedUserId"></param>
    public FollowUserCommand(Guid followedUserId)
    {
        FollowedUserId = followedUserId;
    }

    /// <summary>
    ///     Id do usuário a ser seguido.
    /// </summary>
    public Guid FollowedUserId { get; private set; }
}