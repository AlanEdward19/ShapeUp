namespace NotificationService.Notification.Common.Service;

public interface INotificationConsumer
{
    Task ConsumeNotificationEventsAsync(CancellationToken cancellationToken);
}