using SharedKernel.Utils;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.UploadProfilePicture;

/// <summary>
///     Handler para o comando de upload de foto de perfil.
/// </summary>
/// <param name="repository"></param>
/// <param name="blobStorageProvider"></param>
public class UploadProfilePictureCommandHandler(IProfileGraphRepository repository, IBlobStorageProvider blobStorageProvider)
    : IHandler<bool, UploadProfilePictureCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de upload de foto de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<bool> HandleAsync(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {
        Profile profile = await repository.GetProfileAsync(ProfileContext.ProfileId);

        var containerName = $"{profile.Id}";

        var blobName = blobStorageProvider.SanitizeName(
            $"profile-pictures/{DateTime.Today:yyyy-MM-dd}/{command.ImageHash}.{command.ImageFileName.Split('.').Last()}",
            true);

        await blobStorageProvider.WriteBlobAsync(command.Image, blobName, containerName);

        profile.UpdateImage(blobName);

        await repository.UpdateProfileAsync(profile);

        return true;
    }
}