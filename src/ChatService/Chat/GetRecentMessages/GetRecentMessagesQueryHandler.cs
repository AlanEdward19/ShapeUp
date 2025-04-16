using ChatService.Chat.Common;
using ChatService.Chat.Common.Repository;
using ChatService.Common.Interfaces;

namespace ChatService.Chat.GetRecentMessages;

/// <summary>
/// Handler para a query de mensagens recentes
/// </summary>
/// <param name="repository"></param>
public class GetRecentMessagesQueryHandler(IChatMongoRepository repository) : IHandler<IEnumerable<ChatMessage>, GetRecentMessagesQuery>
{
    /// <summary>
    /// Método para obter mensagens recentes
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ChatMessage>> HandleAsync(GetRecentMessagesQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetRecentMessagesAsync(query.ProfileId, query.Page);
    }
}