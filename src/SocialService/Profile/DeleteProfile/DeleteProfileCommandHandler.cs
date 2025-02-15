using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.DeleteProfile;

/// <summary>
///     Handler para o comando de deletar um perfil
/// </summary>
/// <param name="blobStorageProvider"></param>
/// <param name="graphRepository"></param>
public class DeleteProfileCommandHandler(
    IBlobStorageProvider blobStorageProvider,
    IProfileGraphRepository graphRepository) : IHandler<bool, DeleteProfileCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de deletar um perfil
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<bool> HandleAsync(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.DeleteProfileAsync(command.ProfileId);

        await blobStorageProvider.DeleteContainerAsync(command.ProfileId.ToString());

        return true;
    }
}