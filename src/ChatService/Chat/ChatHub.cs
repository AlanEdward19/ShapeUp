using ChatService.Chat.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Chat;

public class ChatHub(IConfiguration configuration) : Hub
{
    public async Task SendMessage(string senderId, string receiverId, string message)
    {
        string encryptionKey = configuration["EncryptionKey"] ?? throw new ArgumentNullException("EncryptionKey");
        ChatMessage chatMessage = new(Guid.Parse(senderId), Guid.Parse(receiverId), encryptionKey, message);

        await Clients.User(receiverId).SendAsync("ReceiveMessage", chatMessage);
    }
    
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        
        if (userId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        
        if (userId != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}