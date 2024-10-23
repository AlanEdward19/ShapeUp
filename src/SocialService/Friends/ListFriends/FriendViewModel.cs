namespace SocialService.Friends.ListFriends;

public class FriendViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid ProfileId { get; set; }

    public FriendViewModel(string firstName, string lastName, Guid profileId)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileId = profileId;
    }
}