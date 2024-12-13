using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.EditProfile;

/// <summary>
///     Handler para o comando de edição de perfil
/// </summary>
/// <param name="context"></param>
public class EditProfileCommandHandler(IProfileGraphRepository graphRepository) : IHandler<ProfileAggregate, EditProfileCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de edição de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ProfileAggregate> HandleAsync(EditProfileCommand command, CancellationToken cancellationToken)
    {
        Profile profile = await graphRepository.GetProfileAsync(ProfileContext.ProfileId);

        ProfileAggregate profileAggregate = new(profile);

        profileAggregate.UpdateBio(command.Bio);
        profileAggregate.UpdateBirthDate(command.BirthDate);
        profileAggregate.UpdateGender(command.Gender);
        profileAggregate.UpdateCity(command.City);
        profileAggregate.UpdateState(command.State);
        profileAggregate.UpdateCountry(command.Country);

        profile.UpdateBasedOnValueObject(profileAggregate);

        await graphRepository.UpdateProfileAsync(profile);

        return profileAggregate;
    }
}