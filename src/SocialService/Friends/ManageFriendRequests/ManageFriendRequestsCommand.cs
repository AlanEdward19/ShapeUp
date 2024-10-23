namespace SocialService.Friends.ManageFriendRequests;

public class ManageFriendRequestsCommand(bool accept, Guid profileId)
{
    public bool Accept { get; private set; } = accept;
    public Guid ProfileId { get; private set; } = profileId;
}