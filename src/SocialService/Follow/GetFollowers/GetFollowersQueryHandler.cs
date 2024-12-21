using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Follow.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.GetFollowers;

/// <summary>
///     Handler para obter os seguidores de um perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class GetFollowersQueryHandler(IProfileGraphRepository profileGraphRepository, IFollowerGraphRepository graphRepository)
    : IHandler<IEnumerable<ProfileBasicInformation>, GetFollowersQuery>
{
    /// <summary>
    ///     Método para obter os seguidores de um perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformation>> HandleAsync(GetFollowersQuery query,
        CancellationToken cancellationToken)
    {
        var followers = await graphRepository.GetFollowersAsync(query.ProfileId);
        var pagedFollowersIds = followers
            .Skip((query.Page - 1) * query.Rows)
            .Take(query.Rows)
            .Select(Guid.Parse)
            .ToList();

        var profiles = await profileGraphRepository.GetProfilesAsync(pagedFollowersIds);
        
        return profiles
            .Select(x => new ProfileBasicInformation(x.FirstName, x.LastName, x.Id))
            .ToList();
    }
}