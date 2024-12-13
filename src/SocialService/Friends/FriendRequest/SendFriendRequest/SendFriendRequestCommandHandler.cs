using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.FriendRequest.SendFriendRequest;

/// <summary>
///     Handler para adicionar um amigo.
/// </summary>
/// <param name="graphRepository"></param>
public class SendFriendRequestCommandHandler(IFriendshipGraphRepository graphRepository) : IHandler<bool, SendFriendRequestCommand>
{
    /// <summary>
    ///     Método para adicionar um amigo.
    /// </summary>
    /// <param name="requestCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(SendFriendRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        await graphRepository.SendFriendRequestAsync(ProfileContext.ProfileId, requestCommand.FriendId,
            requestCommand.RequestMessage ?? "");

        return true;
    }
}