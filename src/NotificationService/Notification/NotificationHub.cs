using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Notification;

public class NotificationHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
        
        if (!string.IsNullOrEmpty(userId))
        {
            ConnectedUsers[Context.ConnectionId] = userId;
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (ConnectedUsers.TryRemove(Context.ConnectionId, out var userId))
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

        await base.OnDisconnectedAsync(exception);
    }
    
    public static bool IsUserConnected(string userId)
    {
        return ConnectedUsers.Values.Contains(userId);
    }
}