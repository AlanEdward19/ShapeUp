using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.EditPost;

/// <summary>
///     Handler para criação de post.
/// </summary>
/// <param name="repository"></param>
public class EditPostCommandHandler(IPostGraphRepository repository)
    : IHandler<PostDto, EditPostCommand>
{
    /// <summary>
    ///     Método para criação de post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PostDto> HandleAsync(EditPostCommand command, CancellationToken cancellationToken)
    {
        return new(await repository.UpdatePostAsync(command));
    }
}