using Microsoft.EntityFrameworkCore;
using SocialService.Database;
using SocialService.Storage;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.DeleteProfile;

public class DeleteProfileCommandHandler(DatabaseContext context, IStorageProvider storageProvider)
{
    public async Task Handle(DeleteProfileCommand command, CancellationToken cancellationToken)
    {
        var profile = await context.Profiles.FirstAsync(x => x.ObjectId.Equals(command.ProfileId), cancellationToken);

        if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
            await storageProvider.DeleteBlobAsync(profile.ImageUrl, "profile-pictures");
        
        context.Profiles.Remove(profile);
    }
}