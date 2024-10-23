using Microsoft.EntityFrameworkCore;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Sql;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends.CheckFriendRequestStatus;

public class CheckFriendRequestStatusQueryHandler(DatabaseContext context, IMongoContext mongoContext) : IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery>
{
    public async Task<IEnumerable<CheckFriendRequestStatusViewModel>> HandleAsync(CheckFriendRequestStatusQuery query, CancellationToken cancellationToken)
    {
        var profile = await mongoContext.GetProfileDocumentByIdAsync(ProfileContext.ProfileId);
        var invitesSentIdList = profile.InvitesSent.Select(x => Guid.Parse(x.FriendId));
        var invitesReceivedIdList = profile.InvitesReceived.Select(x => Guid.Parse(x.FriendId));

        IEnumerable<Profile.Profile> profiles = await context.Profiles.AsNoTracking()
            .Where(x => invitesReceivedIdList.Concat(invitesSentIdList).Contains(x.ObjectId))
            .ToListAsync(cancellationToken);
        
        var result = profiles.Select(x => new CheckFriendRequestStatusViewModel(x.FirstName, x.LastName,
            invitesSentIdList.Contains(x.ObjectId) ? EFriendStatus.Pending : EFriendStatus.PendingResponse,
            x.ObjectId)).ToList();
        
        return result;
    }
}