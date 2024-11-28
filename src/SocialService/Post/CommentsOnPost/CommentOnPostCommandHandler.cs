using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.CommentsOnPost;

public class CommentOnPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, CommentOnPostCommand>
{
    public async Task<bool> HandleAsync(CommentOnPostCommand command, CancellationToken cancellationToken)
    {
        await repository.CommentOnPostAsync(command, ProfileContext.ProfileId);

        return true;
    }
}