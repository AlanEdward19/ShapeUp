using MongoDB.Driver;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;
using SocialService.Database.Sql;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends.AddFriend;

/// <summary>
/// Handler para adicionar um amigo.
/// </summary>
/// <param name="friendMongoContext"></param>
public class AddFriendCommandHandler(IFriendMongoContext friendMongoContext) : IHandler<bool, AddFriendCommand>
{
    /// <summary>
    /// Método para adicionar um amigo.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(AddFriendCommand command, CancellationToken cancellationToken)
    {
        FriendshipInvite friendshipInvite = new(ProfileContext.ProfileId.ToString(), command.RequestMessage);
        await friendMongoContext.AddFriendshipInviteAsync(command.FriendId, friendshipInvite);

        return true;
    }
}