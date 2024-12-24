using SocialService.Post.Common.Repository;

namespace SocialService.Post.React.GetReactionsOnPost;

/// <summary>
///     Handler para a query de pegar reações em um post.
/// </summary>
/// <param name="repository"></param>
public class GetReactionsOnPostQueryHandler(IPostGraphRepository repository)
    : IHandler<IEnumerable<Reaction>, GetReactionsOnPostQuery>
{
    /// <summary>
    ///     Método para lidar com a query de pegar reações em um post.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Reaction>> HandleAsync(GetReactionsOnPostQuery query,
        CancellationToken cancellationToken)
    {
        return await repository.GetReactionsOnPostAsync(query.PostId);
    }
}