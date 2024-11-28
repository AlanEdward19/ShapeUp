using Microsoft.EntityFrameworkCore;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Sql;
using SocialService.Friends.Common.Enums;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.CheckFriendRequestStatus;

/// <summary>
/// Handler para verificar o status das solicitações de amizade.
/// </summary>
/// <param name="context"></param>
/// <param name="friendMongoContext"></param>
public class CheckFriendRequestStatusQueryHandler(DatabaseContext context, IFriendshipGraphRepository graphRepository) : IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery>
{
    /// <summary>
    /// Método para verificar o status das solicitações de amizade.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CheckFriendRequestStatusViewModel>> HandleAsync(CheckFriendRequestStatusQuery query, CancellationToken cancellationToken)
    {
        var requests = await graphRepository.GetPendingRequestsForProfileAsync(ProfileContext.ProfileId);
        var sentRequests = await graphRepository.GetSentFriendRequestsAsync(ProfileContext.ProfileId);
        
        var invitesSentIdList = sentRequests.Select(x => Guid.Parse( x.SenderProfileId));
        var invitesReceivedIdList =requests.Select(x => Guid.Parse(x.ReceiverProfileId));

        IEnumerable<Profile.Profile> profiles = await context.Profiles.AsNoTracking()
            .Where(x => invitesReceivedIdList.Concat(invitesSentIdList).Contains(x.ObjectId))
            .ToListAsync(cancellationToken);
        
        var result = profiles.Select(x => new CheckFriendRequestStatusViewModel(x.FirstName, x.LastName,
            invitesSentIdList.Contains(x.ObjectId) ? EFriendStatus.Pending : EFriendStatus.PendingResponse,
            x.ObjectId)).ToList();
        
        return result;
    }
}