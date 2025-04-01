namespace SocialService.Friends.FriendRequest.SendFriendRequest;

/// <summary>
///     Comando para adicionar um amigo.
/// </summary>
public class SendFriendRequestCommand
{
    /// <summary>
    ///     Id do amigo.
    /// </summary>
    public string FriendId { get; set; }

    /// <summary>
    ///     Mensagem de solicitação.
    /// </summary>
    public string? RequestMessage { get; set; }
}