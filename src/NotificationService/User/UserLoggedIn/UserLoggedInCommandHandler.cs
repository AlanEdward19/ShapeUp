using NotificationService.Common.Interfaces;
using NotificationService.User.Repository;

namespace NotificationService.User.UserLoggedIn;

/// <summary>
/// Handler para o comando de login do usuário
/// </summary>
/// <param name="repository"></param>
public class UserLoggedInCommandHandler(IUserRepository repository) : IHandler<bool, UserLoggedInCommand>
{
    /// <summary>
    /// Executa o comando de login do usuário
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(UserLoggedInCommand command, CancellationToken cancellationToken)
    {
        await repository.UserLogInAsync(command.UserId, command.DeviceToken, cancellationToken);

        return true;
    }
}