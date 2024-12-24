using SocialService.Post.Common.Repository;

namespace SocialService.Post.GetPost;

/// <summary>
///     Handler para a query de informações de um post
/// </summary>
/// <param name="repository"></param>
public class GetPostQueryHandler(IPostGraphRepository repository) : IHandler<PostDto, GetPostQuery>
{
    /// <summary>
    ///     Método para obter as informações de um post
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<PostDto> HandleAsync(GetPostQuery item, CancellationToken cancellationToken)
    {
        return new PostDto(await repository.GetPostAsync(item.PostId));
    }
}