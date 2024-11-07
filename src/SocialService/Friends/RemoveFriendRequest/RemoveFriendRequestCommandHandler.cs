using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Database.Mongo;
using SocialService.Database.Mongo.Contracts;

namespace SocialService.Friends.RemoveFriendRequest;

public class RemoveFriendRequestCommandHandler(IFriendMongoContext friendMongoContext) : IHandler<bool, RemoveFriendRequestCommand>
{
    public async Task<bool> HandleAsync(RemoveFriendRequestCommand command, CancellationToken cancellationToken)
    {
        await friendMongoContext.RemoveRequestFromProfile(ProfileContext.ProfileId, command.ProfileId, true);
        await friendMongoContext.RemoveRequestFromProfile(command.ProfileId, ProfileContext.ProfileId, false);
        
        return true;
    }
}