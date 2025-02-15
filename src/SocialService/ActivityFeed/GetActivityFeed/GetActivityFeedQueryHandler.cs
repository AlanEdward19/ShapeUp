using SocialService.ActivityFeed.Common.Repository;
using SocialService.Post;

namespace SocialService.ActivityFeed.GetActivityFeed;

/// <summary>
///     Handler para obter o feed de atividades.
/// </summary>
/// <param name="graphRepository"></param>
public class GetActivityFeedQueryHandler(IActivityFeedGraphRepository graphRepository, IBlobStorageProvider blobStorageProvider)
    : IHandler<IEnumerable<PostDto>, GetActivityFeedQuery>
{
    /// <summary>
    ///     Método para obter o feed de atividades.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PostDto>> HandleAsync(GetActivityFeedQuery query,
        CancellationToken cancellationToken)
    {
        IEnumerable<Post.Post> posts = await graphRepository.BuildActivityFeed(query, ProfileContext.ProfileId);
        List<PostDto> result = new(posts.Count());
        
        foreach (var post in posts)
        {
            List<string> imageUrls = new(post.Images.Count());

            foreach (var image in post.Images)
                imageUrls.Add(blobStorageProvider.GenerateAuthenticatedUrl(image, $"{post.PublisherId}"));

            PostDto postDto = new(post);
            postDto.SetImages(imageUrls);
            
            if (!string.IsNullOrWhiteSpace(post.PublisherImageUrl))
                postDto.SetPublisherImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(post.PublisherImageUrl, $"{post.PublisherId}"));
            
            result.Add(postDto);
        }

        return result;
    }
}