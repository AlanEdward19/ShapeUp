using SocialService.Common.Events;

namespace SocialService.Common.Services;

public interface INotificationPublisher
{
    Task PublishNotificationEventAsync(NotificationEvent notificationEvent);
}