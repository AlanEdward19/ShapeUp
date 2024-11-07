using Microsoft.EntityFrameworkCore;
using SocialService.Common;
using SocialService.Common.Interfaces;
using DatabaseContext = SocialService.Database.Sql.DatabaseContext;

namespace SocialService.Profile.EditProfile;

/// <summary>
/// Handler para o comando de edição de perfil
/// </summary>
/// <param name="context"></param>
public class EditProfileCommandHandler(DatabaseContext context) : IHandler<ProfileAggregate, EditProfileCommand>
{
    /// <summary>
    /// Método para lidar com o comando de edição de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    public async Task<ProfileAggregate> HandleAsync(EditProfileCommand command, CancellationToken cancellationToken)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);
        
        Profile profile = await context.Profiles.FirstAsync(
            x => x.ObjectId.Equals(ProfileContext.ProfileId),
            cancellationToken);

        ProfileAggregate profileAggregate = new(profile);

        profileAggregate.UpdateBio(command.Bio);
        profileAggregate.UpdateBirthDate(command.BirthDate);
        profileAggregate.UpdateGender(command.Gender);

        profile.UpdateBasedOnValueObject(profileAggregate);

        context.Profiles.Update(profile);
        
        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);

        return profileAggregate;
    }
}