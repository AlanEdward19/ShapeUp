using SocialService.ActivityFeed.GetActivityFeed;

namespace SocialService.ActivityFeed.Common.Repository;

/// <summary>
/// Interface para o repositório de feed de atividades.
/// </summary>
public interface IActivityFeedRepository
{
    /// <summary>
    /// Método para construir o feed de atividades.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="profileId"></param>
    /// <returns></returns>
    Task<IEnumerable<Post.Post>> BuildActivityFeed(GetActivityFeedQuery query, Guid profileId);
}