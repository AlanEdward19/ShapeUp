using NotificationService.Notification.Common.Event;

namespace NotificationService.Notification.Common.Service;

public interface INotificationProcessor
{
    Task ProcessNotificationAsync(NotificationEvent notificationEvent);
}