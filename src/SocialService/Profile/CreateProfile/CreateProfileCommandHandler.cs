using SharedKernel.Utils;
using SocialService.Common.Services.CepAwesomeApi;
using SocialService.Connections.Search;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Handler para o comando de criação de perfil.
/// </summary>
/// <param name="graphRepository"></param>
public class CreateProfileCommandHandler(IProfileGraphRepository graphRepository, ICepAwesomeApi cepAwesomeApi, IAzureSearchProvider searchProvider)
    : IHandler<ProfileDto, CreateProfileCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de criação de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileDto> HandleAsync(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        var locationInfo = await cepAwesomeApi.GetLocationInfoByPostalCodeAsync(command.PostalCode);
        
        Profile profile = new(command, ProfileContext.ProfileId, locationInfo);

        #region Create Profile Graph

        await graphRepository.CreateProfileAsync(profile);

        #endregion
        
        await searchProvider.UpsertAsync(profile.Id, $"{profile.FirstName} {profile.LastName}");
        
        ProfileDto profileDto = new(profile, locationInfo.State, locationInfo.City);

        return profileDto;
    }
}