using Microsoft.EntityFrameworkCore;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Sql;

namespace SocialService.Friends.ListFriends;

public class ListFriendsQueryHandler(DatabaseContext context, IMongoContext mongoContext) : IHandler<IEnumerable<FriendViewModel>, ListFriendsQuery>
{
    public async Task<IEnumerable<FriendViewModel>> HandleAsync(ListFriendsQuery query,
        CancellationToken cancellationToken)
    {
        var friendsList = await mongoContext.GetProfileFriendListByIdAsync(query.ProfileId);
        var pagedFriendsIds = friendsList
            .Skip((query.Page - 1) * query.Rows).Take(query.Rows).Select(x => Guid.Parse(x.FriendId));

        return await context.Profiles.AsNoTracking().Where(x => pagedFriendsIds.Contains(x.ObjectId))
            .Select(x => new FriendViewModel(x.FirstName, x.LastName, x.ObjectId)).ToListAsync(cancellationToken);
    }
}