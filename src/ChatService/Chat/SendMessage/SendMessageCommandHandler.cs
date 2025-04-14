using ChatService.Chat.Common.Enum;
using ChatService.Chat.Common.Event;
using ChatService.Chat.Common.Repository;
using ChatService.Chat.Common.Service;
using ChatService.Common.Interfaces;

namespace ChatService.Chat.SendMessage;

/// <summary>
/// Handler para o comando de envio de mensagem
/// </summary>
/// <param name="repository"></param>
public class SendMessageCommandHandler(IChatMongoRepository repository, INotificationPublisher notificationPublisher)
    : IHandler<bool, SendMessageCommand>
{
    /// <summary>
    /// Método para enviar uma mensagem
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(SendMessageCommand command, CancellationToken cancellationToken)
    {
        await repository.SendMessageAsync(command.GetSenderId(), command.ReceiverId, command.Message);

        NotificationEvent @event = new()
        {
            RecipientId = command.ReceiverId,
            Topic = ENotificationTopic.Message,
            Content = $"Você recebeu uma mensagem de {command.GetSenderId()}"
        };
        
        await notificationPublisher.PublishNotificationEventAsync(@event);

        return true;
    }
}