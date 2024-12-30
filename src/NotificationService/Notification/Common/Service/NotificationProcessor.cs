using NotificationService.Notification.Common.Event;

namespace NotificationService.Notification.Common.Service;

public class NotificationProcessor(INotificationService service) : INotificationProcessor
{
    public async Task ProcessNotificationAsync(NotificationEvent notificationEvent)
    {
        await service.PublishNotificationAsync(notificationEvent.RecipientId, notificationEvent.Topic.ToString());
    }
}