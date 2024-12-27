using ChatService.Chat.Common.Repository;
using ChatService.Common.Interfaces;

namespace ChatService.Chat.GetRecentMessages;

public class GetRecentMessagesQueryHandler(IChatMongoRepository repository) : IHandler<IEnumerable<ChatMessage>, GetRecentMessagesQuery>
{
    public async Task<IEnumerable<ChatMessage>> HandleAsync(GetRecentMessagesQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetRecentMessagesAsync(query.ProfileId.ToString(), query.Page);
    }
}