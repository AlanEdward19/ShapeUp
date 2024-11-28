using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.AddFriend;

/// <summary>
/// Handler para adicionar um amigo.
/// </summary>
/// <param name="friendMongoContext"></param>
public class AddFriendCommandHandler(IFriendshipGraphRepository graphRepository) : IHandler<bool, AddFriendCommand>
{
    /// <summary>
    /// Método para adicionar um amigo.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(AddFriendCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.SendFriendRequestAsync(ProfileContext.ProfileId, command.FriendId, command.RequestMessage);

        return true;
    }
}