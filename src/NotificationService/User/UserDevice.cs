namespace NotificationService.User;

public class UserDevice(string fcmToken)
{
    public string FcmToken { get; private set; } = fcmToken;
    public DateTime LastAccess { get; private set; } = DateTime.UtcNow;
    
    public void UpdateLastAccess()
    {
        LastAccess = DateTime.UtcNow;
    }
}