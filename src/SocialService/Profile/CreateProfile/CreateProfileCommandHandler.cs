using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Handler para o comando de criação de perfil.
/// </summary>
/// <param name="graphRepository"></param>
public class CreateProfileCommandHandler(IProfileGraphRepository graphRepository)
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
        Profile profile = new(command, ProfileContext.ProfileId);

        #region Create Profile Graph

        await graphRepository.CreateProfileAsync(profile);

        #endregion

        ProfileDto profileDto = new(profile);

        return profileDto;
    }
}