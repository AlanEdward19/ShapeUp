using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;
using MongoDB.Driver;

namespace ChatService.Chat.Common.Repository;

public class ChatMongoRepository(IMongoDatabase database) : IChatMongoRepository
{
    private readonly IMongoCollection<ChatMessage> _chatMessages = database.GetCollection<ChatMessage>("chatMessages");
    
    public async Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(Guid userId, int page)
    {
        return await _chatMessages
            .Find(message => message.ReceiverId == userId || message.SenderId == userId)
            .SortByDescending(message => message.Timestamp)
            .Skip((page - 1) * 50)
            .Limit(50)
            .ToListAsync();
    }

    public async Task SendMessageAsync(Guid senderId, Guid receiverId, string message)
    {
        ChatMessage chatMessage = new(senderId, receiverId, message);
        
        await _chatMessages.InsertOneAsync(chatMessage);
    }
}