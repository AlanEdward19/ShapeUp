using ChatService.Chat.GetRecentMessages;
using ChatService.Chat.SendMessage;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ChatService.Chat.Common.Repository;

public class ChatMongoRepository(IMongoDatabase database, IConfiguration configuration) : IChatMongoRepository
{
    private readonly IMongoCollection<ChatMessage> _chatMessages = database.GetCollection<ChatMessage>("chatMessages");
    
    public async Task<IEnumerable<ChatMessage>> GetRecentMessagesAsync(string userId, int page)
    {
        var result = await _chatMessages
            .Aggregate()
            .Match(message => message.ReceiverId == userId || message.SenderId == userId)
            .Group(
                new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "Participants", new BsonArray { "$SenderId", "$ReceiverId" } }
                        }
                    },
                    { "LastMessage", new BsonDocument("$last", "$$ROOT") }
                })
            .Sort(new BsonDocument("LastMessage.Timestamp", -1))
            .Skip((page - 1) * 20)
            .Limit(20)
            .ToListAsync();

        return result.Select(group => BsonSerializer.Deserialize<ChatMessage>(group["LastMessage"].AsBsonDocument));
    }

    public async Task SendMessageAsync(Guid senderId, Guid receiverId, string message)
    {
        string encryptionKey = configuration["EncryptionKey"] ?? throw new ArgumentNullException("EncryptionKey");
        ChatMessage chatMessage = new(senderId, receiverId, encryptionKey, message);
        
        await _chatMessages.InsertOneAsync(chatMessage);
    }
}