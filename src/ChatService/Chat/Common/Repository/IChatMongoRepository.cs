using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;

namespace ChatService.Chat.Common.Repository;

public interface IChatMongoRepository
{
    Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(Guid userId, int page);
    Task SendMessageAsync(Guid senderId, Guid receiverId, string message);
}