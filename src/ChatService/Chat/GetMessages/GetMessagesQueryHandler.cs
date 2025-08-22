using ChatService.Chat.Common;
using ChatService.Chat.Common.Repository;
using ChatService.Common.Interfaces;

namespace ChatService.Chat.GetMessages;

/// <summary>
/// Handler para a query de mensagens
/// </summary>
/// <param name="repository"></param>
public class GetMessagesQueryHandler(IChatMongoRepository repository)
    : IHandler<IEnumerable<ChatMessage>, GetMessagesQuery>
{
    /// <summary>
    /// Método para obter mensagens
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ChatMessage>> HandleAsync(GetMessagesQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetMessagesAsync(query.ProfileAId, query.ProfileBId, query.Page, query.IsProfessionalChat);
    }
}