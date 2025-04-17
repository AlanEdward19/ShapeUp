using NotificationService.Common.Enums;

namespace NotificationService.User.Repository;

/// <summary>
/// Interface para o repositório de usuários
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Método responsável por adicionar um dispositivo ao usuário ou atualizar o último acesso
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deviceToken"></param>
    /// <param name="devicePlatform"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UserLogInAsync(string userId, string deviceToken, EPlatform devicePlatform,
        CancellationToken cancellationToken);

    /// <summary>
    /// Método responsável por retornar o token do último dispositivo acessado pelo usuário
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetUserLastAccessDeviceTokenAsync(string userId, CancellationToken cancellationToken);
}