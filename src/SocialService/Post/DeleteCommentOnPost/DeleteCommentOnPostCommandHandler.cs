using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Repository;
using SocialService.Post.DeleteCommentOnPost;

namespace SocialService.Post.EditCommentOnPost;

public class DeleteCommentOnPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, DeleteCommentOnPostCommand>
{
    public async Task<bool> HandleAsync(DeleteCommentOnPostCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteCommentOnPostAsync(command.CommentId);
        
        return true;
    }
}