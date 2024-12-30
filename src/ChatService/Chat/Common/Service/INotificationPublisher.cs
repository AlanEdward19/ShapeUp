using ChatService.Chat.Common.Event;

namespace ChatService.Chat.Common.Service;

public interface INotificationPublisher
{
    Task PublishNotificationEventAsync(NotificationEvent notificationEvent);
}