using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;

namespace SocialService.Friends.RemoveFriend;

/// <summary>
/// Handler para remover um amigo.
/// </summary>
/// <param name="friendMongoContext"></param>
public class RemoveFriendCommandHandler(IFriendMongoContext friendMongoContext) : IHandler<bool, RemoveFriendCommand>
{
    /// <summary>
    /// Método para remover um amigo.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(RemoveFriendCommand command, CancellationToken cancellationToken)
    {
        await friendMongoContext.RemoveFriendAsync(ProfileContext.ProfileId, command.ProfileId);

        return true;
    }
}