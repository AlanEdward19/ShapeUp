using SocialService.Follow.Common.Repository;

namespace SocialService.Follow.FollowUser;

/// <summary>
///     Handler para seguir um usuário.
/// </summary>
/// <param name="graphRepository"></param>
public class FollowUserCommandHandler(IFollowerGraphRepository graphRepository) : IHandler<bool, FollowUserCommand>
{
    /// <summary>
    ///     Método para seguir um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(FollowUserCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.FollowAsync(ProfileContext.ProfileId, command.FollowedUserId);

        return true;
    }
}