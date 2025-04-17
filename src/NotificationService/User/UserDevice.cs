using NotificationService.Common.Enums;

namespace NotificationService.User;

public class UserDevice(string fcmToken, EPlatform platform)
{
    public string FcmToken { get; private set; } = fcmToken;
    public EPlatform Platform { get; private set; } = platform;
    public DateTime LastAccess { get; private set; } = DateTime.UtcNow;
    
    public void UpdateLastAccess()
    {
        LastAccess = DateTime.UtcNow;
    }
}