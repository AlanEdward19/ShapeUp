using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;
using SocialService.Database.Sql;

namespace SocialService.Friends.ListFriends;

/// <summary>
/// Handler para a query de listagem de amigos.
/// </summary>
/// <param name="context"></param>
/// <param name="friendMongoContext"></param>
public class ListFriendsQueryHandler(DatabaseContext context, IFriendMongoContext friendMongoContext) : IHandler<IEnumerable<ProfileBasicInformationViewModel>, ListFriendsQuery>
{
    /// <summary>
    /// Método para lidar com a query de listagem de amigos.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformationViewModel>> HandleAsync(ListFriendsQuery query,
        CancellationToken cancellationToken)
    {
        var friendsList = await friendMongoContext.GetProfileFriendListByIdAsync(query.ProfileId);
        var pagedFriendsIds = friendsList
            .Skip((query.Page - 1) * query.Rows).Take(query.Rows).Select(x => Guid.Parse(x.FriendId));

        return await context.Profiles.AsNoTracking().Where(x => pagedFriendsIds.Contains(x.ObjectId))
            .Select(x => new ProfileBasicInformationViewModel(x.FirstName, x.LastName, x.ObjectId)).ToListAsync(cancellationToken);
    }
}