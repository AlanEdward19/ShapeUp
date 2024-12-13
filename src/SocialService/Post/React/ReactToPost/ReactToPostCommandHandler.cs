using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.React.ReactToPost;

/// <summary>
///     Handler para o comando de like em um post.
/// </summary>
/// <param name="repository"></param>
public class ReactToPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, ReactToPostCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de like em um post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(ReactToPostCommand command, CancellationToken cancellationToken)
    {
        Reaction reaction = new(ProfileContext.ProfileId, command);
        
        await repository.ReactToPostAsync(reaction);

        return true;
    }
}