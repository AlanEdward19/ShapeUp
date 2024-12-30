namespace NotificationService.Notification.SendNotification;

public class SendNotificationCommand
{
    public Guid ProfileId { get; private set; }
    public string Message { get; set; }
    
    public void SetProfileId(Guid profileId) => ProfileId = profileId;
}