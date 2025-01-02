using SocialService.Profile.Common.Repository;
using SocialService.Profile.ViewProfile;

namespace SocialService.Profile.ViewProfileSimplified;

/// <summary>
///     Handler para a query de visualização de perfil.
/// </summary>
/// <param name="repository"></param>
public class ViewProfileSimplifiedQueryHandler(IProfileGraphRepository repository, IStorageProvider storageProvider) : IHandler<ProfileSimplifiedDto, ViewProfileSimplifiedQuery>
{
    /// <summary>
    ///     Método para lidar com a query de visualização de perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileSimplifiedDto> HandleAsync(ViewProfileSimplifiedQuery query, CancellationToken cancellationToken)
    {
        Profile profile = await repository.GetProfileAsync(query.ProfileId);
        ProfileSimplifiedDto profileDto = new(profile);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            profileDto.SetImageUrl(storageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}"));

        return profileDto;
    }
}