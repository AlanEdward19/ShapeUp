using System.Text.Json;
using Grpc.Core;
using BDS.DataPack.SharedKernel.Protos;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Connections.Database;
using NotificationService.Connections.Firebase;
using NotificationService.User.Repository;
using SharedKernel.Enums;

namespace NotificationService.Notification.Services;

public class NotificationService(
    IHubContext<NotificationHub> hubContext,
    IUserRepository repository,
    IFcmService fcmService,
    ILogger<NotificationService> logger) : BDS.DataPack.SharedKernel.Protos.NotificationService.NotificationServiceBase
{
    public override async Task<NotificationResponse> sendNotification(NotificationParams request,
        ServerCallContext context)
    {
        bool result = true;
        string userId = request.RecipientId;

        Dictionary<string, string> metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(request.Metadata)!;
        string title = request.Title, body = request.Body;
        ENotificationTopic topic = (ENotificationTopic)request.Topic;

        dynamic notification = new
        {
            topic,
            title,
            body,
            metadata
        };

        string notificationJson = JsonSerializer.Serialize(notification);

        try
        {
            if (NotificationHub.IsUserConnected(userId))
                await hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", notificationJson);

            else
            {
                string deviceToken = await repository.GetUserLastAccessDeviceTokenAsync(userId, CancellationToken.None);

                FcmNotification fcmNotification = new(title, body, metadata);
                await fcmService.SendNotificationAsync(deviceToken, fcmNotification);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error sending notification to user {UserId}", userId);
            result = false;
        }

        return new NotificationResponse
        {
            Success = result
        };
    }
}