using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.DeleteReactionFromPost;

/// <summary>
///     Handler para deletar uma reação de um post
/// </summary>
/// <param name="repository"></param>
public class DeleteReactionFromPostCommandHandler(IPostGraphRepository repository)
    : IHandler<bool, DeleteReactionFromPostCommand>
{
    /// <summary>
    ///     Método para deletar uma reação de um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(DeleteReactionFromPostCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteReactionOnPostAsync(command.PostId, ProfileContext.ProfileId);

        return true;
    }
}