using System.Text.Json;
using Grpc.Core;
using BDS.DataPack.SharedKernel.Protos;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Notification.Services;

public class NotificationService(IHubContext<NotificationHub> hubContext, ILogger<NotificationService> logger) : BDS.DataPack.SharedKernel.Protos.NotificationService.NotificationServiceBase
{
    public override async Task<NotificationResponse> sendNotification(NotificationParams request, ServerCallContext context)
    {
        bool result = true;
        string userId = request.RecipientId;

        dynamic notification = new
        {
            request.Topic,
            request.Content,
            request.Metadata
        };
        
        string notificationJson = JsonSerializer.Serialize(notification);
        
        try
        {
            if (NotificationHub.IsUserConnected(userId))
                await hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", notificationJson);

            else
            {
                //TODO Implementar envio para FCM
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