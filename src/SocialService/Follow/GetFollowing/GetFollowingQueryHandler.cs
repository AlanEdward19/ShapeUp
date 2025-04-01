using SocialService.Follow.Common.Repository;
using SocialService.Profile.Common.Repository;

namespace SocialService.Follow.GetFollowing;

/// <summary>
///     Handler para obter os perfis seguidos de um perfil
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class GetFollowingQueryHandler(
    IProfileGraphRepository profileGraphRepository,
    IFollowerGraphRepository graphRepository, IBlobStorageProvider blobStorageProvider)
    : IHandler<IEnumerable<ProfileBasicInformation>, GetFollowingQuery>
{
    /// <summary>
    ///     Método para obter os perfis seguidos de um perfil
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProfileBasicInformation>> HandleAsync(GetFollowingQuery query,
        CancellationToken cancellationToken)
    {
        var following = await graphRepository.GetFollowingAsync(query.ProfileId);
        var pagedFollowingIds = following
            .Skip((query.Page - 1) * query.Rows)
            .Take(query.Rows)
            .ToList();

        var profiles = await profileGraphRepository.GetProfilesAsync(pagedFollowingIds);
        List<ProfileBasicInformation> result = new(profiles.Count());

        foreach (var profile in profiles)
        {
            string imageUrl = string.IsNullOrWhiteSpace(profile.ImageUrl)
                ? string.Empty
                : blobStorageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}");

            result.Add(new ProfileBasicInformation(profile.FirstName, profile.LastName, profile.Id, imageUrl));
        }

        return result;
    }
}