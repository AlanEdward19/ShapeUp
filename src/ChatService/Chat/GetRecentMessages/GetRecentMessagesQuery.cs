namespace ChatService.Chat.GetRecentMessages;

public class GetRecentMessagesQuery
{
    public Guid ProfileId { get; private set; }
    public int Page { get; private set; }
    
    public void SetProfileId(Guid profileId) => ProfileId = profileId;
    public void SetPage(int page) => Page = page;
}