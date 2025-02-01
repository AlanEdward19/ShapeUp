using SocialService.Common.Services.BrasilApi;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Handler para o comando de criação de perfil.
/// </summary>
/// <param name="graphRepository"></param>
public class CreateProfileCommandHandler(IProfileGraphRepository graphRepository, IBrasilApi brasilApi)
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
        var locationInfo = await brasilApi.GetLocationInfoByPostalCodeAsync(command.PostalCode);
        
        Profile profile = new(command, ProfileContext.ProfileId, locationInfo);

        #region Create Profile Graph

        await graphRepository.CreateProfileAsync(profile);

        #endregion
        
        ProfileDto profileDto = new(profile, locationInfo.State, locationInfo.City);

        return profileDto;
    }
}