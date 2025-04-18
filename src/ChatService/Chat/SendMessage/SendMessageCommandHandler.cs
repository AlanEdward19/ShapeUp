using ChatService.Chat.Common.Repository;
using ChatService.Common.Interfaces;
using SharedKernel.Dtos;
using SharedKernel.Enums;
using SharedKernel.Providers;

namespace ChatService.Chat.SendMessage;

/// <summary>
/// Handler para o comando de envio de mensagem
/// </summary>
/// <param name="repository"></param>
public class SendMessageCommandHandler(IChatMongoRepository repository, IGrpcProvider grpcProvider)
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
        
        NotificationDto notificationDto = new()
        {
            RecipientId = command.ReceiverId,
            Title = "Nova mensagem",
            Topic = ENotificationTopic.FriendRequest,
            Body = "Você recebeu uma nova mensagem\"",
            Metadata = new()
            {
                { "userId",command.ReceiverId.ToString() }
            }
        };

        await grpcProvider.SendNotification(notificationDto, cancellationToken);

        return true;
    }
}