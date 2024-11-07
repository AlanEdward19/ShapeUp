using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo.Contracts;

namespace SocialService.Follow.FollowUser;

/// <summary>
/// Handler para seguir um usuário.
/// </summary>
/// <param name="followerMongoContext"></param>
public class FollowUserCommandHandler(IFollowerMongoContext followerMongoContext) : IHandler<bool, FollowUserCommand>
{
    /// <summary>
    /// Método para seguir um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(FollowUserCommand command, CancellationToken cancellationToken)
    {
        await followerMongoContext.FollowProfileAsync(ProfileContext.ProfileId, command.FollowedUserId);
        
        return true;
    }
}