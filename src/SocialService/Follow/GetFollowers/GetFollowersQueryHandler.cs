using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Connections.Sql;
using SocialService.Follow.Common.Repository;

namespace SocialService.Follow.GetFollowers;

/// <summary>
///     Handler para obter os seguidores de um perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class GetFollowersQueryHandler(DatabaseContext context, IFollowerGraphRepository graphRepository)
    : IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowersQuery>
{
    /// <summary>
    ///     Método para obter os seguidores de um perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformationViewModel>> HandleAsync(GetFollowersQuery query,
        CancellationToken cancellationToken)
    {
        var followers = await graphRepository.GetFollowersAsync(query.ProfileId);
        var pagedFollowersIds = followers
            .Skip((query.Page - 1) * query.Rows).Take(query.Rows).Select(Guid.Parse);

        return await context.Profiles.AsNoTracking().Where(x => pagedFollowersIds.Contains(x.ObjectId))
            .Select(x => new ProfileBasicInformationViewModel(x.FirstName, x.LastName, x.ObjectId))
            .ToListAsync(cancellationToken);
    }
}