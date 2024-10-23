namespace SocialService.Friends.RemoveFriend;

public class RemoveFriendCommand
{
    public Guid ProfileId { get; private set; }

    public RemoveFriendCommand(Guid profileId)
    {
        ProfileId = profileId;
    }
}