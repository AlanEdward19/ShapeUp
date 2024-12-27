namespace ChatService.Chat.SendMessage;

/// <summary>
/// Comando para enviar uma mensagem
/// </summary>
/// <param name="receiverId"></param>
/// <param name="message"></param>
public class SendMessageCommand(Guid receiverId, string message)
{
    /// <summary>
    /// Id do perfil que enviou a mensagem
    /// </summary>
    public Guid SenderId { get; private set; }
    
    /// <summary>
    /// Id do perfil que recebeu a mensagem
    /// </summary>
    public Guid ReceiverId { get; private set; } = receiverId;
    
    /// <summary>
    /// Mensagem a ser enviada
    /// </summary>
    public string Message { get; private set; } = message;
    
    /// <summary>
    /// Método para definir o Id do perfil que enviou a mensagem
    /// </summary>
    /// <param name="senderId"></param>
    public void SetSenderId(Guid senderId) => SenderId = senderId;
}