using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Connections.Sql;
using SocialService.Follow.Common.Repository;

namespace SocialService.Follow.GetFollowing;

/// <summary>
///     Handler para obter os perfis seguidos de um perfil
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class GetFollowingQueryHandler(DatabaseContext context, IFollowerGraphRepository graphRepository)
    : IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowingQuery>
{
    /// <summary>
    ///     Método para obter os perfis seguidos de um perfil
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformationViewModel>> HandleAsync(GetFollowingQuery query,
        CancellationToken cancellationToken)
    {
        var following = await graphRepository.GetFollowingAsync(query.ProfileId);
        var pagedFollowingIds = following
            .Skip((query.Page - 1) * query.Rows).Take(query.Rows).Select(Guid.Parse);

        return await context.Profiles.AsNoTracking().Where(x => pagedFollowingIds.Contains(x.ObjectId))
            .Select(x => new ProfileBasicInformationViewModel(x.FirstName, x.LastName, x.ObjectId))
            .ToListAsync(cancellationToken);
    }
}