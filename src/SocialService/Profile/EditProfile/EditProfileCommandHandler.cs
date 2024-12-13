using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.EditProfile;

/// <summary>
///     Handler para o comando de edição de perfil
/// </summary>
/// <param name="context"></param>
public class EditProfileCommandHandler(IProfileGraphRepository graphRepository) : IHandler<ProfileDto, EditProfileCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de edição de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ProfileDto> HandleAsync(EditProfileCommand command, CancellationToken cancellationToken)
    {
        Profile profile = await graphRepository.GetProfileAsync(ProfileContext.ProfileId);

        ProfileDto profileDto = new(profile);

        profileDto.UpdateBio(command.Bio);
        profileDto.UpdateBirthDate(command.BirthDate);
        profileDto.UpdateGender(command.Gender);
        profileDto.UpdateCity(command.City);
        profileDto.UpdateState(command.State);
        profileDto.UpdateCountry(command.Country);

        profile.UpdateBasedOnValueObject(profileDto);

        await graphRepository.UpdateProfileAsync(profile);

        return profileDto;
    }
}