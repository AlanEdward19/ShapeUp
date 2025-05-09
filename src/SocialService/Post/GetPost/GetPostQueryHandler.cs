using SharedKernel.Utils;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.GetPost;

/// <summary>
///     Handler para a query de informações de um post
/// </summary>
/// <param name="repository"></param>
public class GetPostQueryHandler(IPostGraphRepository repository, IBlobStorageProvider blobStorageProvider) : IHandler<PostDto, GetPostQuery>
{
    /// <summary>
    ///     Método para obter as informações de um post
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PostDto> HandleAsync(GetPostQuery query, CancellationToken cancellationToken)
    {
        Post post = await repository.GetPostAsync(query.PostId, ProfileContext.ProfileId);
        List<string> imageUrls = new(post.Images.Count());
        
        foreach (var image in post.Images)
            imageUrls.Add(blobStorageProvider.GenerateAuthenticatedUrl(image, $"{post.PublisherId}"));
        
        PostDto postDto = new(post);
        postDto.SetImages(imageUrls);
        
        if(!string.IsNullOrWhiteSpace(post.PublisherImageUrl))
            postDto.SetPublisherImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(post.PublisherImageUrl, $"{post.PublisherId}"));
        
        return postDto;
    }
}