namespace SocialService.Post.DeleteReactionFromPost;

public class DeleteReactionFromPostCommand
{
    public Guid PostId { get; private set; }
    
    public void SetPostId(Guid postId) => PostId = postId;
}