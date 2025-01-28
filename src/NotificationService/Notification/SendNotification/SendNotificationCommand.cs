using NotificationService.Notification.Common.Enums;

namespace NotificationService.Notification.SendNotification;

public class SendNotificationCommand
{
    public Guid ProfileId { get; private set; }
    public ENotificationTopic Topic { get; set; }
    public string Message { get; set; }
    
    public void SetProfileId(Guid profileId) => ProfileId = profileId;
}