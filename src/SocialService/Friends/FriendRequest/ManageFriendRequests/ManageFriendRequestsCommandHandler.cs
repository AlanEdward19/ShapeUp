using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.FriendRequest.ManageFriendRequests;

/// <summary>
///     Handler para gerenciar solicitações de amizade.
/// </summary>
/// <param name="graphRepository"></param>
public class ManageFriendRequestsCommandHandler(IFriendshipGraphRepository graphRepository)
    : IHandler<bool, ManageFriendRequestsCommand>
{
    /// <summary>
    ///     Método para gerenciar solicitações de amizade.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(ManageFriendRequestsCommand command, CancellationToken cancellationToken)
    {
        if (command.Accept)
            await graphRepository.AcceptFriendRequestAsync(command.ProfileId, ProfileContext.ProfileId);

        else
            await graphRepository.RejectFriendRequestAsync(command.ProfileId, ProfileContext.ProfileId);

        return true;
    }
}