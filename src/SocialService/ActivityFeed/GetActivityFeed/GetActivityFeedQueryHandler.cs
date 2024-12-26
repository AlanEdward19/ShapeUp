using SocialService.ActivityFeed.Common.Repository;
using SocialService.Post;

namespace SocialService.ActivityFeed.GetActivityFeed;

/// <summary>
///     Handler para obter o feed de atividades.
/// </summary>
/// <param name="graphRepository"></param>
public class GetActivityFeedQueryHandler(IActivityFeedGraphRepository graphRepository)
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
        return (await graphRepository.BuildActivityFeed(query, ProfileContext.ProfileId))
            .Select(x => new PostDto(x));
    }
}