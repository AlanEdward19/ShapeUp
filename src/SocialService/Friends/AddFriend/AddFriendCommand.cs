namespace SocialService.Friends.AddFriend;

/// <summary>
///     Comando para adicionar um amigo.
/// </summary>
public class AddFriendCommand
{
    /// <summary>
    ///     Id do amigo.
    /// </summary>
    public Guid FriendId { get; set; }

    /// <summary>
    ///     Mensagem de solicitação.
    /// </summary>
    public string? RequestMessage { get; set; }
}