using SharedKernel.Utils;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.CreatePost;

/// <summary>
///     Handler para criação de post.
/// </summary>
/// <param name="repository"></param>
public class CreatePostCommandHandler(IPostGraphRepository repository, IBlobStorageProvider blobStorageProvider)
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
        string profileId = ProfileContext.ProfileId;
        Post post = new(command, profileId);

        var blobName = $"post-images/{post.Id}";

        await blobStorageProvider.CreateFolderAsync(blobName, profileId);
        await repository.CreatePostAsync(ProfileContext.ProfileId, post);

        return new PostDto(post);
    }
}