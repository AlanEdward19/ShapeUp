using SharedKernel.Utils;
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
        Post post = await repository.GetPostAsync(command.PostId, ProfileContext.ProfileId);

        post.UpdateContent(command.Content);
        post.UpdateVisibility(command.Visibility);

        await repository.UpdatePostAsync(post);

        return new PostDto(post);
    }
}