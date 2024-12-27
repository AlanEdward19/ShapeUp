namespace ChatService.Chat.SendMessage;

public class SendMessageCommand(Guid senderId, Guid receiverId, string message)
{
    public Guid SenderId { get; private set; } = senderId;
    public Guid ReceiverId { get; private set; } = receiverId;
    public string Message { get; private set; } = message;
}