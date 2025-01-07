using SocialService.Post.Common.Repository;

namespace SocialService.Post.GetPostsByProfileId;

/// <summary>
///     Handler para a query de obter posts pelo id do perfil
/// </summary>
/// <param name="repository"></param>
public class GetPostsByProfileIdQueryHandler(IPostGraphRepository repository, IStorageProvider storageProvider)
    : IHandler<IEnumerable<PostDto>, GetPostsByProfileIdQuery>
{
    /// <summary>
    ///     Método para obter os posts de um perfil
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PostDto>> HandleAsync(GetPostsByProfileIdQuery query,
        CancellationToken cancellationToken)
    {
        IEnumerable<Post> posts =
            await repository.GetPostsByProfileIdAsync(query.ProfileId, ProfileContext.ProfileId, query.Page,
                query.Rows);
        List<PostDto> result = new(posts.Count());

        foreach (var post in posts)
        {
            List<string> imageUrls = new(post.Images.Count());

            foreach (var image in post.Images)
                imageUrls.Add(storageProvider.GenerateAuthenticatedUrl(image, $"{post.PublisherId}"));

            PostDto postDto = new(post);
            postDto.SetImages(imageUrls);

            if (!string.IsNullOrWhiteSpace(post.PublisherImageUrl))
                postDto.SetPublisherImageUrl(
                    storageProvider.GenerateAuthenticatedUrl(post.PublisherImageUrl, $"{post.PublisherId}"));

            result.Add(postDto);
        }

        return result;
    }
}