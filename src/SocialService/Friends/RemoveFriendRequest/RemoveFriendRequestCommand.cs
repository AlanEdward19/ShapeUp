namespace SocialService.Friends.RemoveFriendRequest;

public class RemoveFriendRequestCommand
{
    public Guid ProfileId { get; private set; }

    public RemoveFriendRequestCommand(Guid profileId)
    {
        ProfileId = profileId;
    }
}