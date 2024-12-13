using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Connections.Storage;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.UploadProfilePicture;

/// <summary>
///     Handler para o comando de upload de foto de perfil.
/// </summary>
/// <param name="repository"></param>
/// <param name="storageProvider"></param>
public class UploadProfilePictureCommandHandler(IProfileGraphRepository repository, IStorageProvider storageProvider)
    : IHandler<bool, UploadProfilePictureCommand>
{
    /// <summary>
    /// Método para lidar com o comando de upload de foto de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<bool> HandleAsync(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {
        Profile profile = await repository.GetProfileAsync(ProfileContext.ProfileId);

        ProfileDto profileDto = new(profile);

        var containerName = $"{profileDto.Id}";

        var blobName =
            $"profile-pictures/{DateTime.Today:yyyy-MM-dd}/{Guid.NewGuid()}.{command.ImageFileName.Split('.').Last()}";

        await storageProvider.WriteBlobAsync(command.Image, blobName, containerName);

        profile.UpdateImage(blobName);

        await repository.UpdateProfileAsync(profile);

        return true;
    }
}