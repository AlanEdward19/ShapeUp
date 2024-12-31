using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ChatService.Chat.Common.Repository;

/// <summary>
/// Repositório para mensagens de chat
/// </summary>
/// <param name="database"></param>
/// <param name="configuration"></param>
public class ChatMongoRepository(IMongoDatabase database, IHubContext<ChatHub> hubContext, IConfiguration configuration)
    : IChatMongoRepository
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
            .Sort(new BsonDocument("Timestamp", -1))
            .Group(
                new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "Participants", new BsonArray 
                                { 
                                    new BsonDocument("$cond", new BsonArray 
                                    { 
                                        new BsonDocument("$lt", new BsonArray { "$SenderId", "$ReceiverId" }), 
                                        new BsonArray { "$SenderId", "$ReceiverId" }, 
                                        new BsonArray { "$ReceiverId", "$SenderId" } 
                                    }) 
                                } 
                            }
                        }
                    },
                    { "LastMessage", new BsonDocument("$first", "$$ROOT") }
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
            .Find(message => (message.ReceiverId == userId && message.SenderId == otherUserId) ||
                             (message.ReceiverId == otherUserId && message.SenderId == userId))
            .SortByDescending(message => message.Timestamp)
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
        
        await hubContext.Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", chatMessage);
    }
}