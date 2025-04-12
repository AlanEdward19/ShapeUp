namespace SocialService.Follow.UnfollowUser;

/// <summary>
///     Comando para deixar de seguir um usuário.
/// </summary>
public class UnfollowUserCommand
{
    /// <summary>
    ///     Construtor do comando.
    /// </summary>
    /// <param name="unfollowedUserId"></param>
    public UnfollowUserCommand(string unfollowedUserId)
    {
        UnfollowedUserId = unfollowedUserId;
    }

    /// <summary>
    ///     Id do usuário que será deixado de seguir.
    /// </summary>
    public string UnfollowedUserId { get; private set; }
}