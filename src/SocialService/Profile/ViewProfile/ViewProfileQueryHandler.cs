using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.ViewProfile;

/// <summary>
///     Handler para a query de visualização de perfil.
/// </summary>
/// <param name="repository"></param>
public class ViewProfileQueryHandler(IProfileGraphRepository repository, IStorageProvider storageProvider)
    : IHandler<ProfileDto, ViewProfileQuery>
{
    /// <summary>
    ///     Método para lidar com a query de visualização de perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileDto> HandleAsync(ViewProfileQuery query, CancellationToken cancellationToken)
    {
        Profile profile = await repository.GetProfileAsync(query.ProfileId);

        ProfileDto profileDto = new(profile);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            profileDto.SetImageUrl(storageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}"));

        return profileDto;
    }
}