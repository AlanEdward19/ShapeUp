using System.Security.Cryptography;
using System.Text;
using ChatService.Encryption;

namespace ChatService.Chat.GetRecentMessages;

public class ChatMessage
{
    private string _encryptionKey;
    
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SenderId { get; private set; }
    public string ReceiverId { get; private set; }
    public string EncryptedMessage { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.Now;

    public ChatMessage(Guid senderId, Guid receiverId, string encryptionKey, string message)
    {
        SenderId = senderId.ToString();
        ReceiverId = receiverId.ToString();
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