namespace NotificationService.Notification.Common.Service;

public interface INotificationService
{
    Task PublishNotificationAsync(Guid userId, string message);
    Task<List<string>> GetPendingNotificationsAsync(Guid userId);
}