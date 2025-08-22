namespace ChatService.Chat.SendMessage;

/// <summary>
/// Comando para enviar uma mensagem
/// </summary>
/// <param name="receiverId"></param>
/// <param name="message"></param>
public class SendMessageCommand(string receiverId, string message)
{
    /// <summary>
    /// Id do perfil que enviou a mensagem
    /// </summary>
    private string SenderId { get; set; }
    
    /// <summary>
    /// Id do perfil que recebeu a mensagem
    /// </summary>
    public string ReceiverId { get; private set; } = receiverId;
    
    /// <summary>
    /// Mensagem a ser enviada
    /// </summary>
    public string Message { get; private set; } = message;
    
    /// <summary>
    /// Se a mensagem é de um chat de um profissional
    /// </summary>
    private bool IsProfessionalChat { get; set; }
    
    /// <summary>
    /// Método para definir o Id do perfil que enviou a mensagem
    /// </summary>
    /// <param name="senderId"></param>
    public void SetSenderId(string senderId) => SenderId = senderId;
    
    /// <summary>
    /// Método para obter o Id do perfil que enviou a mensagem
    /// </summary>
    /// <returns></returns>
    public string GetSenderId() => SenderId;
    
    /// <summary>
    /// Método para definir se a mensagem é de um chat de um profissional
    /// </summary>
    /// <param name="isProfessionalChat"></param>
    public void SetIsProfessionalChat(bool isProfessionalChat) => IsProfessionalChat = isProfessionalChat;
    
    /// <summary>
    /// Método para obter se a mensagem é de um chat de um profissional
    /// </summary>
    /// <returns></returns>
    public bool GetIsProfessionalChat() => IsProfessionalChat;
}