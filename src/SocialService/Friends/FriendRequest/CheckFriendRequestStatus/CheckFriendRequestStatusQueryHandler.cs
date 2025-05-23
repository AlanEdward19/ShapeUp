﻿using SharedKernel.Utils;
using SocialService.Friends.Common.Enums;
using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.FriendRequest.CheckFriendRequestStatus;

/// <summary>
///     Handler para verificar o status das solicitações de amizade.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class CheckFriendRequestStatusQueryHandler(
    IProfileGraphRepository profileGraphRepository,
    IFriendshipGraphRepository graphRepository)
    : IHandler<IEnumerable<CheckFriendRequestStatusViewModel>, CheckFriendRequestStatusQuery>
{
    /// <summary>
    ///     Método para verificar o status das solicitações de amizade.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CheckFriendRequestStatusViewModel>> HandleAsync(CheckFriendRequestStatusQuery query,
        CancellationToken cancellationToken)
    {
        var requests = await graphRepository.GetPendingRequestsForProfileAsync(ProfileContext.ProfileId);
        var sentRequests = await graphRepository.GetSentFriendRequestsAsync(ProfileContext.ProfileId);

        var invitesSentIdList = sentRequests
            .Select(x => x.ReceiverProfileId)
            .ToList();
        var invitesReceivedIdList = requests
            .Select(x => x.SenderProfileId)
            .ToList();
        var clearList = invitesSentIdList
            .Concat(invitesReceivedIdList)
            .Distinct()
            .ToList();

        IEnumerable<Profile.Profile> profiles = await profileGraphRepository.GetProfilesAsync(clearList);

        var result = profiles.Select(x => new CheckFriendRequestStatusViewModel(x.FirstName, x.LastName,
            invitesSentIdList.Contains(x.Id) ? EFriendStatus.Pending : EFriendStatus.PendingResponse,
            x.Id)).ToList();

        return result;
    }
}