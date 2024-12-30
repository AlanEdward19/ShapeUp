using System.Text.Json;
using NotificationService.Notification.Common.Enums;
using NotificationService.Notification.Common.Event;
using StackExchange.Redis;

namespace NotificationService.Notification.Common.Service;

public class NotificationConsumer(IConnectionMultiplexer redis, INotificationProcessor notificationProcessor) : INotificationConsumer
{
    public async Task ConsumeNotificationEventsAsync(CancellationToken cancellationToken)
    {
        var db = redis.GetDatabase();

        while (!cancellationToken.IsCancellationRequested)
        {
            var entries = await db.StreamReadGroupAsync(
                "notification-events",
                "notification-group",
                "notification-consumer",
                count: 10,
                noAck: false
            );

            foreach (var entry in entries)
            {
                var notificationEvent = new NotificationEvent
                {
                    RecipientId = Guid.Parse(entry["RecipientId"]),
                    Topic = Enum.Parse<ENotificationTopic>(entry["Topic"]),
                    Content = entry["Content"],
                    Metadata = JsonSerializer.Deserialize<object>(entry["Metadata"])
                };

                await notificationProcessor.ProcessNotificationAsync(notificationEvent);

                // Marca a mensagem como processada
                await db.StreamAcknowledgeAsync("notification-events", "notification-group", entry.Id);
            }

            await Task.Delay(1000); // Evita sobrecarga no Redis
        }
    }
}