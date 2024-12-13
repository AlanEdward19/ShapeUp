using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Friends.Common.Repository;

namespace SocialService.Friends.Friendship.RemoveFriend;

/// <summary>
///     Handler para remover um amigo.
/// </summary>
/// <param name="graphRepository"></param>
public class RemoveFriendCommandHandler(IFriendshipGraphRepository graphRepository)
    : IHandler<bool, RemoveFriendCommand>
{
    /// <summary>
    ///     Método para remover um amigo.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(RemoveFriendCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.UnfriendAsync(ProfileContext.ProfileId, command.ProfileId);

        return true;
    }
}