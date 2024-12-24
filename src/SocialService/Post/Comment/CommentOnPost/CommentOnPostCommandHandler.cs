using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.CommentOnPost;

/// <summary>
///     Handler para comentar em um post
/// </summary>
/// <param name="repository"></param>
public class CommentOnPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, CommentOnPostCommand>
{
    /// <summary>
    ///     Método para comentar em um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(CommentOnPostCommand command, CancellationToken cancellationToken)
    {
        Comment comment = new(command, ProfileContext.ProfileId);

        await repository.CommentOnPostAsync(comment);

        return true;
    }
}