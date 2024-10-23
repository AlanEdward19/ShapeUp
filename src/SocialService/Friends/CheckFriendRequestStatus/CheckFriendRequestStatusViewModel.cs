using SocialService.Friends.Common.Enums;

namespace SocialService.Friends.CheckFriendRequestStatus;

public class CheckFriendRequestStatusViewModel
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public EFriendStatus Status { get; private set; }
    public Guid ProfileId { get; private set; }

    public CheckFriendRequestStatusViewModel(string firstName, string lastName, EFriendStatus status, Guid profileId)
    {
        FirstName = firstName;
        LastName = lastName;
        Status = status;
        ProfileId = profileId;
    }
}