using Microsoft.EntityFrameworkCore;
using SocialService.Common;
using SocialService.Database;
using SocialService.Storage;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.UploadProfilePicture;

public class UploadProfilePictureCommandHandler(DatabaseContext context, IStorageProvider storageProvider)
{
    public async Task Handle(UploadProfilePictureCommand command, CancellationToken cancellationToken)
    {
        var containerName = "profile-pictures";
        string blobName;

        Profile profile = await context.Profiles.FirstAsync(
            x => x.ObjectId.Equals(ProfileContext.ProfileId),
            cancellationToken);

        ProfileAggregate profileAggregate = new(profile);

        if (string.IsNullOrWhiteSpace(profileAggregate.ImageUrl))
        {
            blobName = $"{profileAggregate.ObjectId}.{command.ImageFileName.Split('.').Last()}";
        }

        else
        {
            blobName = profileAggregate.ImageUrl;
            var format = profileAggregate.ImageUrl.Split('.').Last();

            if (!format.Equals(command.ImageFileName.Split('.').Last()))
            {
                string oldFileName = profileAggregate.ImageUrl.Split(".").First();
                blobName = $"{oldFileName}.{command.ImageFileName.Split('.').Last()}";
            }

            await storageProvider.DeleteBlobAsync(profileAggregate.ImageUrl, containerName);
        }

        await storageProvider.WriteBlobAsync(command.Image, blobName, containerName);
        
        profileAggregate.UpdateImage(blobName);
        
        profile.UpdateBasedOnValueObject(profileAggregate);
        
        context.Profiles.Update(profile);

        //return profileAggregate;
    }
}