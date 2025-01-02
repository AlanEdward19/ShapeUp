using SocialService.Friends.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.Friendship.ListFriends;

/// <summary>
///     Handler para a query de listagem de amigos.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class ListFriendsQueryHandler(
    IProfileGraphRepository profileGraphRepository,
    IFriendshipGraphRepository graphRepository, IStorageProvider storageProvider)
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
        List<ProfileBasicInformation> result = new(profiles.Count());

        foreach (var profile in profiles)
        {
            string imageUrl = string.IsNullOrWhiteSpace(profile.ImageUrl)
                ? string.Empty
                : storageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}");

            result.Add(new ProfileBasicInformation(profile.FirstName, profile.LastName, profile.Id, imageUrl));
        }

        return result;
    }
}