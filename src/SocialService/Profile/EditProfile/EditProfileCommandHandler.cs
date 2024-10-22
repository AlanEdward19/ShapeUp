using Microsoft.EntityFrameworkCore;
using SocialService.Common;
using SocialService.Database;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.EditProfile;

public class EditProfileCommandHandler(DatabaseContext context)
{
    public async Task Handle(EditProfileCommand command, CancellationToken cancellationToken)
    {
        Profile profile = await context.Profiles.FirstAsync(
            x => x.ObjectId.Equals(ProfileContext.ProfileId),
            cancellationToken);

        ProfileAggregate profileAggregate = new(profile);

        profileAggregate.UpdateBio(command.Bio);
        profileAggregate.UpdateBirthDate(command.BirthDate);
        profileAggregate.UpdateEmail(command.Email);
        profileAggregate.UpdateGender(command.Gender);

        profile.UpdateBasedOnValueObject(profileAggregate);

        context.Profiles.Update(profile);

        //return ProfileAggregate;
    }
}