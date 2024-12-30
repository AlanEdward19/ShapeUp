﻿using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;

namespace NotificationService.Notification.Common.Service;

public class NotificationService(IHubContext<NotificationHub> hubContext, IConnectionMultiplexer redis)
    : INotificationService
{
    
    public async Task PublishNotificationAsync(Guid userId, string message)
    {
        // Enviar para usuários online
        await hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveNotification", message);

        // Salvar no Redis para usuários offline
        var db = redis.GetDatabase();
        await db.ListRightPushAsync($"notifications:{userId}", JsonSerializer.Serialize(message));
    }
    
    public async Task<List<string>> GetPendingNotificationsAsync(Guid userId)
    {
        var db = redis.GetDatabase();
        var notifications = await db.ListRangeAsync($"notifications:{userId}");

        // Remover notificações do Redis após entregá-las
        await db.KeyDeleteAsync($"notifications:{userId}");

        return notifications
            .Select(n => JsonSerializer.Deserialize<string>(n))
            .Distinct()
            .ToList();
    }
}