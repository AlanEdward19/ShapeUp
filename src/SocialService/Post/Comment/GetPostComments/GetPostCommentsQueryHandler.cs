using SocialService.Common.Interfaces;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.GetPostComments;

/// <summary>
///     Handler para obter os comentários de um post
/// </summary>
/// <param name="repository"></param>
public class GetPostCommentsQueryHandler(IPostGraphRepository repository)
    : IHandler<IEnumerable<Comment>, GetPostCommentsQuery>
{
    /// <summary>
    ///     Método para obter os comentários de um post
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Comment>> HandleAsync(GetPostCommentsQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetPostCommentsAsync(query.PostId);
    }
}