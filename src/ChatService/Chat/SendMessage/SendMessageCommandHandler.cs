using ChatService.Chat.Common.Repository;
using ChatService.Common.Interfaces;

namespace ChatService.Chat.SendMessage;

public class SendMessageCommandHandler(IChatMongoRepository repository) : IHandler<bool, SendMessageCommand>
{
    public async Task<bool> HandleAsync(SendMessageCommand command, CancellationToken cancellationToken)
    {
        await repository.SendMessageAsync(command.SenderId, command.SenderId, command.Message);

        return true;
    }
}