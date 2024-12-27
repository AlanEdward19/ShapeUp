using ChatService.Chat.Common.Repository;
using ChatService.Common.Interfaces;

namespace ChatService.Chat.SendMessage;

/// <summary>
/// Handler para o comando de envio de mensagem
/// </summary>
/// <param name="repository"></param>
public class SendMessageCommandHandler(IChatMongoRepository repository) : IHandler<bool, SendMessageCommand>
{
    /// <summary>
    /// Método para enviar uma mensagem
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(SendMessageCommand command, CancellationToken cancellationToken)
    {
        await repository.SendMessageAsync(command.SenderId, command.SenderId, command.Message);

        return true;
    }
}