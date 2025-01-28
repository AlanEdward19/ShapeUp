using Microsoft.AspNetCore.SignalR;

namespace ChatService.Chat;

public class ChatHub(IConfiguration configuration) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        var profileId = Context.GetHttpContext()?.Request.Query["profileId"].ToString();
        
        
        if (userId != null && profileId != null)
        {
            string groupName = GetGroupName(userId, profileId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        var profileId = Context.GetHttpContext()?.Request.Query["profileId"].ToString();
        
        
        if (userId != null && profileId != null)
        {
            string groupName = GetGroupName(userId, profileId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        
        await base.OnDisconnectedAsync(exception);
    }
    
    private string GetGroupName(string userId1, string userId2)
    {
        var orderedIds = new[] { userId1, userId2 }.OrderBy(id => id);
        return string.Join("-", orderedIds);
    }
}