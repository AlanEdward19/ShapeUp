namespace NotificationService.Connections.Firebase;

public class FcmNotification(string title, string body, Dictionary<string, string> data)
{
    public string Title { get; private set; } = title;
    public string Body { get; private set; } = body;
    public Dictionary<string, string> Data { get; set; } = data;
}