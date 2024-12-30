using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ChatService.Chat.Common.Repository;

/// <summary>
/// Repositório para mensagens de chat
/// </summary>
/// <param name="database"></param>
/// <param name="configuration"></param>
public class ChatMongoRepository(IMongoDatabase database, IConfiguration configuration) : IChatMongoRepository
{
    private readonly IMongoCollection<ChatMessage> _chatMessages = database.GetCollection<ChatMessage>("chatMessages");
    
    /// <summary>
    /// Método para obter mensagens recentes
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Método para obter mensagens entre dois perfis
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="otherUserId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(string userId, string otherUserId, int page)
    {
        return await _chatMessages
            .Find(message => (message.ReceiverId == userId && message.SenderId == otherUserId) || (message.ReceiverId == otherUserId && message.SenderId == userId))
            .SortBy(message => message.Timestamp)
            .Skip((page - 1) * 100)
            .Limit(100)
            .ToListAsync();
    }

    /// <summary>
    /// Método para enviar uma mensagem
    /// </summary>
    /// <param name="senderId"></param>
    /// <param name="receiverId"></param>
    /// <param name="message"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task SendMessageAsync(Guid senderId, Guid receiverId, string message)
    {
        string encryptionKey = configuration["EncryptionKey"] ?? throw new ArgumentNullException("EncryptionKey");
        ChatMessage chatMessage = new(senderId, receiverId, encryptionKey, message);
        
        await _chatMessages.InsertOneAsync(chatMessage);
    }
}