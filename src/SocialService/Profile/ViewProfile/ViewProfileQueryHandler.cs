using SocialService.Common.Services.BrasilApi;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;

namespace SocialService.Profile.ViewProfile;

/// <summary>
///     Handler para a query de visualização de perfil.
/// </summary>
/// <param name="repository"></param>
public class ViewProfileQueryHandler(
    IProfileGraphRepository repository,
    IBrasilApi brasilApi,
    IBlobStorageProvider blobStorageProvider)
    : IHandler<ProfileDto?, ViewProfileQuery>
{
    /// <summary>
    ///     Método para lidar com a query de visualização de perfil.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileDto?> HandleAsync(ViewProfileQuery query, CancellationToken cancellationToken)
    {
        Profile? profile = await repository.GetProfileAsync(query.ProfileId);

        if (profile is null)
            return null;

        var locationInfo = await brasilApi.GetLocationInfoByPostalCodeAsync(profile.PostalCode);
        
        ProfileDto profileDto = new(profile, locationInfo.State, locationInfo.City);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            profileDto.SetImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}"));

        return profileDto;
    }
}