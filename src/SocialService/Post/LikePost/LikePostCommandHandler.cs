using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.LikePost;

/// <summary>
/// Handler para o comando de like em um post.
/// </summary>
/// <param name="repository"></param>
public class LikePostCommandHandler(IPostGraphRepository repository) : IHandler<bool, LikePostCommand>
{
    /// <summary>
    /// Método para lidar com o comando de like em um post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(LikePostCommand command, CancellationToken cancellationToken)
    {
        await repository.LikePostAsync(command.PostId, ProfileContext.ProfileId, command.ReactionType);

        return true;
    }
}