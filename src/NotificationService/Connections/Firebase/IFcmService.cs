namespace NotificationService.Connections.Firebase;

public interface IFcmService
{
    Task SendNotificationAsync(string fcmToken, FcmNotification notification);
}