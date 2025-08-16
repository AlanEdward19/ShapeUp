using SharedKernel.Utils;
using SocialService.Common.Services.CepAwesomeApi;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;

namespace SocialService.Profile.ViewProfile;

/// <summary>
///     Handler para a query de visualização de perfil.
/// </summary>
/// <param name="repository"></param>
public class ViewProfileQueryHandler(
    IProfileGraphRepository repository,
    ICepAwesomeApi cepAwesomeApi,
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
        Profile? profile = await repository.GetProfileAsync(query.ProfileId, ProfileContext.ProfileId);

        if (profile is null)
            return null;

        string state = "", city = "";
        try
        {
            var locationInfo = await cepAwesomeApi.GetLocationInfoByPostalCodeAsync(profile.PostalCode);
            city = locationInfo.City;
            state = locationInfo.State;
        }
        catch
        {
        }

        ProfileDto profileDto = new(profile, state, city);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            profileDto.SetImageUrl(blobStorageProvider.GenerateAuthenticatedUrl(profile.ImageUrl, $"{profile.Id}"));

        return profileDto;
    }
}