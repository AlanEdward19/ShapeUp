namespace ChatService.Chat.GetRecentMessages;

public class ChatMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string EncryptedMessage { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

    public ChatMessage(Guid senderId, Guid receiverId, string message)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        EncryptedMessage = EncryptMessage(message);
    }
    
    private string EncryptMessage(string message)
    {
        return message;
    }
}