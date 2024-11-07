using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;
using SocialService.Database.Sql;
using SocialService.Follow;
using SocialService.Friends;

namespace SocialService.Profile.CreateProfile;

/// <summary>
/// Handler para o comando de criação de perfil.
/// </summary>
/// <param name="context"></param>
/// <param name="friendMongoContext"></param>
public class CreateProfileCommandHandler(DatabaseContext context, IFriendMongoContext friendMongoContext, IFollowerMongoContext followerMongoContext)
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

        #region Build Friend Document

        Friend friend = new(ProfileContext.ProfileId);
        await friendMongoContext.AddProfileDocumentAsync(friend);

        #endregion

        #region Build Follow/Follower Document

        ProfileConnections profileConnections = new(ProfileContext.ProfileId);
        await followerMongoContext.AddProfileDocumentAsync(profileConnections);

        #endregion

        ProfileAggregate profileAggregate = new(profile);

        await context.SaveChangesAsync(cancellationToken);
        await context.Database.CommitTransactionAsync(cancellationToken);

        return profileAggregate;
    }
}