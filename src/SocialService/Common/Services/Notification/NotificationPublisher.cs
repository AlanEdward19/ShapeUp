using System.Text.Json;
using SocialService.Common.Events;
using StackExchange.Redis;

namespace SocialService.Common.Services;

public class NotificationPublisher(IConnectionMultiplexer redis) : INotificationPublisher
{
    public async Task PublishNotificationEventAsync(NotificationEvent notificationEvent)
    {
        var db = redis.GetDatabase();
        // Cria o consumer group se não existir
        try
        {
            await db.StreamCreateConsumerGroupAsync("notification-events", "notification-group", "0-0", createStream: true);
        }
        catch (RedisServerException ex) when (ex.Message.Contains("BUSYGROUP Consumer Group name already exists"))
        {
            // O consumer group já existe, então podemos ignorar essa exceção
        }

        var eventId = await db.StreamAddAsync("notification-events", new[]
        {
            new NameValueEntry("RecipientId", notificationEvent.RecipientId.ToString()),
            new NameValueEntry("Topic", notificationEvent.Topic.ToString()),
            new NameValueEntry("Content", notificationEvent.Content),
            new NameValueEntry("Metadata", JsonSerializer.Serialize(notificationEvent.Metadata))
        });
    }
}
