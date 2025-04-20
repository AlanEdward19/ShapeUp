using SharedKernel.Utils;
using SocialService.Common.Services.BrasilApi;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.EditProfile;

/// <summary>
///     Handler para o comando de edição de perfil
/// </summary>
/// <param name="graphRepository"></param>
public class EditProfileCommandHandler(IProfileGraphRepository graphRepository, IBrasilApi brasilApi)
    : IHandler<ProfileDto, EditProfileCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de edição de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ProfileDto> HandleAsync(EditProfileCommand command, CancellationToken cancellationToken)
    {
        Profile profile = (await graphRepository.GetProfileAsync(ProfileContext.ProfileId))!;

        profile.UpdateBio(command.Bio);
        profile.UpdateBirthDate(command.BirthDate);
        profile.UpdateGender(command.Gender);

        await graphRepository.UpdateProfileAsync(profile);
        
        var locationInfo = await brasilApi.GetLocationInfoByPostalCodeAsync(profile.PostalCode);

        return new(profile, locationInfo.State, locationInfo.City);
    }
}