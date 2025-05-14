using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NotificationService.Connections.Firebase;

public class FcmService(IConfiguration configuration) : IFcmService
{
    private readonly string _serverKey = configuration["Firebase:ServerKey"] ?? throw new ArgumentNullException("Firebase:ServerKey");
    private readonly string _fcmUrl = "https://fcm.googleapis.com/fcm/send";

    public async Task SendNotificationAsync(string fcmToken, FcmNotification notification)
    {
        var payload = new
        {
            to = fcmToken,
            notification = new
            {
                title = notification.Title,
                body = notification.Body
            },
            data = notification.Data
        };

        var json = JsonSerializer.Serialize(payload);
        var request = new HttpRequestMessage(HttpMethod.Post, _fcmUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("key", "=" + _serverKey);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = new HttpClient();
        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao enviar notificação: {error}");
        }
    }
}