using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Sql;
using SocialService.Friends;

namespace SocialService.Profile.CreateProfile;

/// <summary>
/// Handler para o comando de criação de perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="mongoContext"></param>
public class CreateProfileCommandHandler(DatabaseContext context, IMongoContext<Friend> mongoContext)
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

        Friend friend = new(ProfileContext.ProfileId);
        await mongoContext.AddDocumentAsync(friend);

        ProfileAggregate profileAggregate = new(profile);

        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);

        return profileAggregate;
    }
}