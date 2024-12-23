using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.EditProfile;

/// <summary>
///     Handler para o comando de edição de perfil
/// </summary>
/// <param name="graphRepository"></param>
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

        profile.UpdateBio(command.Bio);
        profile.UpdateBirthDate(command.BirthDate);
        profile.UpdateGender(command.Gender);

        await graphRepository.UpdateProfileAsync(profile);

        return new(profile);
    }
}