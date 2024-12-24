using SocialService.Follow.Common.Repository;

namespace SocialService.Follow.UnfollowUser;

/// <summary>
///     Handler para o comando de unfollow de um usuário.
/// </summary>
/// <param name="graphRepository"></param>
public class UnfollowUserCommandHandler(IFollowerGraphRepository graphRepository) : IHandler<bool, UnfollowUserCommand>
{
    /// <summary>
    ///     Método para realizar o unfollow de um usuário.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(UnfollowUserCommand command, CancellationToken cancellationToken)
    {
        await graphRepository.UnfollowAsync(ProfileContext.ProfileId, command.UnfollowedUserId);

        return true;
    }
}