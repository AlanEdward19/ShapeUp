using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Common.Models;
using SocialService.Database.Sql;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.ListFriends;

/// <summary>
/// Handler para a query de listagem de amigos.
/// </summary>
/// <param name="context"></param>
/// <param name="friendMongoContext"></param>
public class ListFriendsQueryHandler(DatabaseContext context, IFriendshipGraphRepository graphRepository) : IHandler<IEnumerable<ProfileBasicInformationViewModel>, ListFriendsQuery>
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
        var friendsList = await graphRepository.GetFriendshipsForProfileAsync(query.ProfileId);
        var pagedFriends = friendsList
            .Skip((query.Page - 1) * query.Rows).Take(query.Rows).ToList();

        var pagedFriendsIds = pagedFriends
            .Select(x => Guid.Parse(x.ProfileAId))
            .Concat(pagedFriends
                .Select(x => Guid.Parse(x.ProfileBId)))
            .Distinct()
            .Where(x => x != query.ProfileId)
            .ToList();

        return await context.Profiles.AsNoTracking().Where(x => pagedFriendsIds.Contains(x.ObjectId))
            .Select(x => new ProfileBasicInformationViewModel(x.FirstName, x.LastName, x.ObjectId)).ToListAsync(cancellationToken);
    }
}