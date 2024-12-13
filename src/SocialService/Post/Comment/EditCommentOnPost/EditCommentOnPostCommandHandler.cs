using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.EditCommentOnPost;

/// <summary>
///     Handler para editar um comentário em um post
/// </summary>
/// <param name="repository"></param>
public class EditCommentOnPostCommandHandler(IPostGraphRepository repository) : IHandler<bool, EditCommentOnPostCommand>
{
    /// <summary>
    ///     Método para editar um comentário em um post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(EditCommentOnPostCommand command, CancellationToken cancellationToken)
    {
        await repository.EditCommentOnPostAsync(command);

        return true;
    }
}