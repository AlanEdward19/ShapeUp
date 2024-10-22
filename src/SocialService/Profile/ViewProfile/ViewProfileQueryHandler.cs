using Microsoft.EntityFrameworkCore;
using SocialService.Database;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.ViewProfile;

public class ViewProfileQueryHandler(DatabaseContext context)
{
    public async Task<ProfileAggregate> Handle(ViewProfileQuery query, CancellationToken cancellationToken)
    {
        Profile profile = await context.Profiles.FirstAsync(
            x => x.ObjectId.Equals(query.ProfileId),
            cancellationToken);

        return new ProfileAggregate(profile);
    }
}