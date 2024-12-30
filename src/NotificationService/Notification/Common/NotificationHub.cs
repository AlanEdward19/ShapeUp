using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Web;
using NotificationService.Notification.Common.Service;

namespace NotificationService.Notification.Common;

public class NotificationHub(INotificationService notificationService) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
        
        if (!string.IsNullOrEmpty(userId))
        {
            // Adicionar conexão do usuário ao grupo
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);

            // Recuperar notificações pendentes e enviar
            var pendingNotifications = await notificationService.GetPendingNotificationsAsync(Guid.Parse(userId));
            foreach (var notification in pendingNotifications)
                await Clients.Caller.SendAsync("ReceiveNotification", notification);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User!.GetObjectId();
        
        if (!string.IsNullOrEmpty(userId))
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

        await base.OnDisconnectedAsync(exception);
    }
}