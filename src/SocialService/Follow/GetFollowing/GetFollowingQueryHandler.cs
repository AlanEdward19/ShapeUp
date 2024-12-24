using SocialService.Follow.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.GetFollowing;

/// <summary>
///     Handler para obter os perfis seguidos de um perfil
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class GetFollowingQueryHandler(
    IProfileGraphRepository profileGraphRepository,
    IFollowerGraphRepository graphRepository)
    : IHandler<IEnumerable<ProfileBasicInformation>, GetFollowingQuery>
{
    /// <summary>
    ///     Método para obter os perfis seguidos de um perfil
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformation>> HandleAsync(GetFollowingQuery query,
        CancellationToken cancellationToken)
    {
        var following = await graphRepository.GetFollowingAsync(query.ProfileId);
        var pagedFollowingIds = following
            .Skip((query.Page - 1) * query.Rows)
            .Take(query.Rows)
            .Select(Guid.Parse)
            .ToList();

        var profiles = await profileGraphRepository.GetProfilesAsync(pagedFollowingIds);

        return profiles
            .Select(x => new ProfileBasicInformation(x.FirstName, x.LastName, x.Id))
            .ToList();
    }
}