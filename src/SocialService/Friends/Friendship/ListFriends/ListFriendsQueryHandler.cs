﻿using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.Friendship.ListFriends;

/// <summary>
///     Handler para a query de listagem de amigos.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class ListFriendsQueryHandler(
    IProfileGraphRepository profileGraphRepository,
    IFriendshipGraphRepository graphRepository)
    : IHandler<IEnumerable<ProfileBasicInformation>, ListFriendsQuery>
{
    /// <summary>
    ///     Método para lidar com a query de listagem de amigos.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformation>> HandleAsync(ListFriendsQuery query,
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

        var profiles = await profileGraphRepository.GetProfilesAsync(pagedFriendsIds);

        return profiles
            .Select(x => new ProfileBasicInformation(x.FirstName, x.LastName, x.Id))
            .ToList();
    }
}