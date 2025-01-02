using SocialService.Follow.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.GetFollowers;

/// <summary>
///     Handler para obter os seguidores de um perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class GetFollowersQueryHandler(
    IProfileGraphRepository profileGraphRepository,
    IFollowerGraphRepository graphRepository,
    IStorageProvider storageProvider)
    : IHandler<IEnumerable<ProfileBasicInformation>, GetFollowersQuery>
{
    /// <summary>
    ///     Método para obter os seguidores de um perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformation>> HandleAsync(GetFollowersQuery query,
        CancellationToken cancellationToken)
    {
        var followers = await graphRepository.GetFollowersAsync(query.ProfileId);
        var pagedFollowersIds = followers
            .Skip((query.Page - 1) * query.Rows)
            .Take(query.Rows)
            .Select(Guid.Parse)
            .ToList();

        var profiles = await profileGraphRepository.GetProfilesAsync(pagedFollowersIds);
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