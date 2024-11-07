namespace SocialService.Follow.UnfollowUser;

/// <summary>
/// Comando para deixar de seguir um usuário.
/// </summary>
public class UnfollowUserCommand
{
    /// <summary>
    /// Id do usuário que será deixado de seguir.
    /// </summary>
    public Guid UnfollowedUserId { get; private set; }

    /// <summary>
    /// Construtor do comando.
    /// </summary>
    /// <param name="unfollowedUserId"></param>
    public UnfollowUserCommand(Guid unfollowedUserId)
    {
        UnfollowedUserId = unfollowedUserId;
    }
}