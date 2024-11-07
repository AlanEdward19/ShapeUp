using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;
using SocialService.Database.Sql;

namespace SocialService.Friends.ManageFriendRequests;

/// <summary>
/// Handler para gerenciar solicitações de amizade.
/// </summary>
/// <param name="friendMongoContext"></param>
public class ManageFriendRequestsCommandHandler(IFriendMongoContext friendMongoContext) : IHandler<bool, ManageFriendRequestsCommand>
{
    /// <summary>
    /// Método para gerenciar solicitações de amizade.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(ManageFriendRequestsCommand command, CancellationToken cancellationToken)
    {
        if (command.Accept)
            await friendMongoContext.AcceptFriendshipInviteAsync(ProfileContext.ProfileId, command.ProfileId);
        
        else
            await friendMongoContext.DeclineFriendshipInviteAsync(ProfileContext.ProfileId, command.ProfileId);

        return true;
    }
}