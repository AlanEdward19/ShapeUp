using System.Security.Cryptography;
using System.Text;
using ChatService.Encryption;

namespace ChatService.Chat.Common;

/// <summary>
/// Classe para representar uma mensagem de chat
/// </summary>
public class ChatMessage
{
    private readonly string _encryptionKey;
    
    /// <summary>
    /// Id da mensagem
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Id do perfil que enviou a mensagem
    /// </summary>
    public string SenderId { get; private set; }
    
    /// <summary>
    /// Id do perfil que recebeu a mensagem
    /// </summary>
    public string ReceiverId { get; private set; }
    
    /// <summary>
    /// Mensagem criptografada
    /// </summary>
    public string EncryptedMessage { get; private set; }
    
    /// <summary>
    /// Timestamp da mensagem
    /// </summary>
    public DateTime Timestamp { get; private set; } = DateTime.Now;

    /// <summary>
    /// Construtor da classe
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <param name="encryptionKey"></param>
    /// <param name="message"></param>
    public ChatMessage(string senderId, string receiverId, string encryptionKey, string message)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        _encryptionKey = encryptionKey;
        EncryptedMessage = EncryptMessage(message);
    }
    
    private string EncryptMessage(string message)
    {
        byte[] key;
        
        using (var sha256 = SHA256.Create())
            key = sha256.ComputeHash(Encoding.UTF8.GetBytes(_encryptionKey));

        return EncryptionService.Encrypt($"{message} {Timestamp}", key);
    }
}