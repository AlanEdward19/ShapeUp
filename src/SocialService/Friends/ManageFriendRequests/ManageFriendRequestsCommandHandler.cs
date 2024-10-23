using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Sql;

namespace SocialService.Friends.ManageFriendRequests;

public class ManageFriendRequestsCommandHandler(IMongoContext mongoContext) : IHandler<bool, ManageFriendRequestsCommand>
{
    public async Task<bool> HandleAsync(ManageFriendRequestsCommand command, CancellationToken cancellationToken)
    {
        if (command.Accept)
            await mongoContext.AcceptFriendshipInviteAsync(ProfileContext.ProfileId, command.ProfileId);
        
        else
            await mongoContext.DeclineFriendshipInviteAsync(ProfileContext.ProfileId, command.ProfileId);

        return true;
    }
}