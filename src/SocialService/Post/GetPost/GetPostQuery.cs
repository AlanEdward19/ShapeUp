namespace SocialService.Post.GetPostInformations;

public class GetPostQuery
{
    public Guid PostId { get; private set; }
    
    public void SetPostId(Guid postId) => PostId = postId;
}