using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo.Contracts;

namespace SocialService.Follow.UnfollowUser;

/// <summary>
/// Handler para o comando de unfollow de um usuário.
/// </summary>
/// <param name="followerMongoContext"></param>
public class UnfollowUserCommandHandler(IFollowerMongoContext followerMongoContext) : IHandler<bool, UnfollowUserCommand>
{
    /// <summary>
    /// Método para realizar o unfollow de um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(UnfollowUserCommand command, CancellationToken cancellationToken)
    {
        await followerMongoContext.UnfollowProfileAsync(ProfileContext.ProfileId, command.UnfollowedUserId);
        
        return true;
    }
}