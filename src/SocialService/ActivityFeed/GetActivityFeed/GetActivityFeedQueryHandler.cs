using SocialService.ActivityFeed.Common.Repository;
using SocialService.Common;
using SocialService.Common.Interfaces;

namespace SocialService.ActivityFeed.GetActivityFeed;

/// <summary>
/// Handler para obter o feed de atividades.
/// </summary>
/// <param name="graphRepository"></param>
public class GetActivityFeedQueryHandler(IActivityFeedGraphRepository graphRepository) : IHandler<IEnumerable<Post.Post>, GetActivityFeedQuery>
{
    /// <summary>
    /// Método para obter o feed de atividades.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Post.Post>> HandleAsync(GetActivityFeedQuery query, CancellationToken cancellationToken)
    {
        return await graphRepository.BuildActivityFeed(query, ProfileContext.ProfileId);
    }
}