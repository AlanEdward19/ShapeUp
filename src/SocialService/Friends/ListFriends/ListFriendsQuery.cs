namespace SocialService.Friends.ListFriends;

public class ListFriendsQuery
{
    public Guid ProfileId { get; private set; }
    public int Page { get; private set; }
    public int Rows { get; private set; }
    
    public void SetProfileId(Guid profileId)
    {
        ProfileId = profileId;
    }
    
    public void SetPage(int page)
    {
        Page = page;
    }
    
    public void SetRows(int rows)
    {
        Rows = rows;
    }
}