using SharedKernel.Utils;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.FriendRequest.RemoveFriendRequest;

/// <summary>
///     Handler para remover um pedido de amizade
/// </summary>
/// <param name="graphRepository"></param>
public class RemoveFriendRequestCommandHandler(IFriendshipGraphRepository graphRepository)
    : IHandler<bool, RemoveFriendRequestCommand>
{
    /// <summary>
    ///     Método para remover um pedido de amizade
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(RemoveFriendRequestCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.RejectFriendRequestAsync(ProfileContext.ProfileId, command.ProfileId);

        return true;
    }
}