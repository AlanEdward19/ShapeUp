using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Sql;
using SocialService.Profile.Common.Repository;

namespace SocialService.Profile.CreateProfile;

/// <summary>
/// Handler para o comando de criação de perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="friendMongoContext"></param>
public class CreateProfileCommandHandler(DatabaseContext context, IProfileGraphRepository graphRepository) 
    : IHandler<ProfileAggregate, CreateProfileCommand>
{
    /// <summary>
    /// Método para lidar com o comando de criação de perfil.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ProfileAggregate> HandleAsync(CreateProfileCommand command, CancellationToken cancellationToken)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);

        Profile profile = new(command, ProfileContext.ProfileId);

        await context.Profiles.AddAsync(profile, cancellationToken);

        #region Create Profile Graph

        await graphRepository.CreateProfileAsync(ProfileContext.ProfileId);

        #endregion

        ProfileAggregate profileAggregate = new(profile);

        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);

        return profileAggregate;
    }
}