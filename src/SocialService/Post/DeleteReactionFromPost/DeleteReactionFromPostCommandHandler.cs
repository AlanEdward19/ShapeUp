using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.DeleteReactionFromPost;

public class DeleteReactionFromPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, DeleteReactionFromPostCommand>
{
    public async Task<bool> HandleAsync(DeleteReactionFromPostCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteReactionOnPostAsync(command.PostId, ProfileContext.ProfileId);
        
        return true;
    }
}