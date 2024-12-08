using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.EditCommentOnPost;

public class EditCommentOnPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, EditCommentOnPostCommand>
{
    public async Task<bool> HandleAsync(EditCommentOnPostCommand command, CancellationToken cancellationToken)
    {
        await repository.EditCommentOnPostAsync(command);
        
        return true;
    }
}