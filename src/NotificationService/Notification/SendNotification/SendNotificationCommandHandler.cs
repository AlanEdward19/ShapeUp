using NotificationService.Common.Interfaces;
using NotificationService.Notification.Common;
using NotificationService.Notification.Common.Service;

namespace NotificationService.Notification.SendNotification;

public class SendNotificationCommandHandler(INotificationService service) : IHandler<bool, SendNotificationCommand>
{
    public async Task<bool> HandleAsync(SendNotificationCommand command, CancellationToken cancellationToken)
    {
        await service.PublishNotificationAsync(command.ProfileId, command.Message);
        
        return true;
    }
}