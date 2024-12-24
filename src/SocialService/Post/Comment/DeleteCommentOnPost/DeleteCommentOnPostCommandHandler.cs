using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.DeleteCommentOnPost;

/// <summary>
///     Handler para deletar um comentário em um post
/// </summary>
/// <param name="repository"></param>
public class DeleteCommentOnPostCommandHandler(IPostGraphRepository repository)
    : IHandler<bool, DeleteCommentOnPostCommand>
{
    /// <summary>
    ///     Método para deletar um comentário em um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(DeleteCommentOnPostCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteCommentOnPostAsync(command.CommentId);

        return true;
    }
}