using Microsoft.EntityFrameworkCore;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Connections.Storage;
using DatabaseContext = SocialService.Connections.Sql.DatabaseContext;

namespace SocialService.Profile.UploadProfilePicture;

/// <summary>
///     Handler para o comando de upload de foto de perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="storageProvider"></param>
public class UploadProfilePictureCommandHandler(DatabaseContext context, IStorageProvider storageProvider)
    : IHandler<bool, UploadProfilePictureCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de upload de foto de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<bool> HandleAsync(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);

        Profile profile = await context.Profiles.FirstAsync(
            x => x.ObjectId.Equals(ProfileContext.ProfileId),
            cancellationToken);

        ProfileAggregate profileAggregate = new(profile);

        var containerName = $"{profileAggregate.ObjectId}";

        var blobName =
            $"profile-pictures/{DateTime.Today:yyyy-MM-dd}/{Guid.NewGuid()}.{command.ImageFileName.Split('.').Last()}";

        await storageProvider.WriteBlobAsync(command.Image, blobName, containerName);

        profileAggregate.UpdateImage(blobName);

        profile.UpdateBasedOnValueObject(profileAggregate);

        context.Profiles.Update(profile);

        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);

        return true;
    }
}