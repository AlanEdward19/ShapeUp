using SocialService.Post.Common.Repository;

namespace SocialService.Post.CreatePost;

/// <summary>
///     Handler para criação de post.
/// </summary>
/// <param name="repository"></param>
public class CreatePostCommandHandler(IPostGraphRepository repository, IStorageProvider storageProvider)
    : IHandler<PostDto, CreatePostCommand>
{
    /// <summary>
    ///     Método para criação de post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PostDto> HandleAsync(CreatePostCommand command, CancellationToken cancellationToken)
    {
        Post post = new(command);

        var blobName = $"post-images/{post.Id}";
        var containerName = ProfileContext.ProfileId.ToString();

        await storageProvider.CreateFolderAsync(blobName, containerName);
        await repository.CreatePostAsync(ProfileContext.ProfileId, post);

        return new PostDto(post);
    }
}