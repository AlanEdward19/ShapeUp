using NotificationService.Common.Interfaces;
using NotificationService.User.Repository;
using NotificationService.User.UserLoggedIn;

namespace NotificationService.User.UserSignOut;

/// <summary>
/// Handler para o comando para remover o dispositivo do usuário
/// </summary>
/// <param name="repository"></param>
public class UserSignOutCommandHandler(IUserRepository repository) : IHandler<bool, UserSignOutCommand>
{
    /// <summary>
    /// Executa o comando para remover o dispositivo do usuário
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> HandleAsync(UserSignOutCommand command, CancellationToken cancellationToken)
    {
        await repository.UserSignOutAsync(command.UserId, command.DeviceToken, cancellationToken);

        return true;
    }
}