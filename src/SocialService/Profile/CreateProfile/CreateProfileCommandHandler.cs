using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.CreateProfile;

/// <summary>
///     Handler para o comando de criação de perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="graphRepository"></param>
public class CreateProfileCommandHandler(IProfileGraphRepository graphRepository)
    : IHandler<ProfileAggregate, CreateProfileCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de criação de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileAggregate> HandleAsync(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        Profile profile = new(command, ProfileContext.ProfileId);

        #region Create Profile Graph

        await graphRepository.CreateProfileAsync(profile);

        #endregion

        ProfileAggregate profileAggregate = new(profile);
        
        return profileAggregate;
    }
}