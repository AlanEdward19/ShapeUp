using NotificationService.Common.Enums;

namespace NotificationService.User.UserLoggedIn;

/// <summary>
/// Comando para registrar o login do usuário
/// </summary>
/// <param name="userId"></param>
/// <param name="deviceToken"></param>
/// <param name="devicePlatform"></param>
public class UserLoggedInCommand(string deviceToken, EPlatform devicePlatform)
{
    public string UserId { get; private set; }
    public string DeviceToken { get; set; } = deviceToken;
    public EPlatform DevicePlatform { get; set; } = devicePlatform;
    
    /// <summary>
    /// Método para definir o ID do usuário
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}