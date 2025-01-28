namespace NotificationService.Notification.Common.Service;

public interface INotificationService
{
    Task PublishNotificationAsync(Guid userId, string topic, string message);
    Task<List<string>> GetPendingNotificationsAsync(Guid userId);
}