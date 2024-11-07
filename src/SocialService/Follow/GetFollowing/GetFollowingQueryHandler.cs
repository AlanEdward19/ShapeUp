using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Database.Mongo.Contracts;
using SocialService.Database.Sql;
using SocialService.Follow.GetFollowers;

namespace SocialService.Follow.GetFollowing;

/// <summary>
/// Handler para obter os perfis seguidos de um perfil
/// </summary>
/// <param name="context"></param>
/// <param name="followerMongoContext"></param>
public class GetFollowingQueryHandler(DatabaseContext context,IFollowerMongoContext followerMongoContext) : IHandler<IEnumerable<ProfileBasicInformationViewModel>, GetFollowingQuery>
{
    /// <summary>
    /// Método para obter os perfis seguidos de um perfil
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformationViewModel>> HandleAsync(GetFollowingQuery query, CancellationToken cancellationToken)
    {
        var profile = await followerMongoContext.GetProfileDocumentByIdAsync(query.ProfileId);
        var pagedFollowingIds = profile.Following
            .Skip((query.Page - 1) * query.Rows).Take(query.Rows).Select(Guid.Parse);
        
        return await context.Profiles.AsNoTracking().Where(x => pagedFollowingIds.Contains(x.ObjectId))
            .Select(x => new ProfileBasicInformationViewModel(x.FirstName, x.LastName, x.ObjectId)).ToListAsync(cancellationToken);
    }
}