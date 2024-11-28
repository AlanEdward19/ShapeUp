using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;
using SocialService.Storage;

namespace SocialService.Post.CreatePost;

/// <summary>
/// Handler para criação de post.
/// </summary>
/// <param name="repository"></param>
/// <param name="storageProvider"></param>
public class CreatePostCommandHandler(IPostGraphRepository repository, IStorageProvider storageProvider)
    : IHandler<Post, CreatePostCommand>
{
    /// <summary>
    /// Método para criação de post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Post> HandleAsync(CreatePostCommand command, CancellationToken cancellationToken)
    {
        Guid postId = Guid.NewGuid();

        await repository.CreatePostAsync(ProfileContext.ProfileId, postId, command);

        return new(postId);
    }
}