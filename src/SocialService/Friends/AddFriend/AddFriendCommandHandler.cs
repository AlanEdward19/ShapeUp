using MongoDB.Driver;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Sql;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends.AddFriend;

public class AddFriendCommandHandler(IMongoContext mongoContext) : IHandler<bool, AddFriendCommand>
{
    public async Task<bool> HandleAsync(AddFriendCommand command, CancellationToken cancellationToken)
    {
        FriendshipInvite friendshipInvite = new(ProfileContext.ProfileId.ToString(), command.RequestMessage);
        await mongoContext.AddFriendshipInviteAsync(command.FriendId, friendshipInvite);

        return true;
    }
}