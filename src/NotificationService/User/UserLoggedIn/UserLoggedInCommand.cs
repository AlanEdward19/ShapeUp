namespace NotificationService.User.UserLoggedIn;

/// <summary>
/// Comando para registrar o login do usuário
/// </summary>
/// <param name="deviceToken"></param>
public class UserLoggedInCommand(string deviceToken)
{
    public string UserId { get; private set; } = "";
    public string DeviceToken { get; set; } = deviceToken;
    
    /// <summary>
    /// Método para definir o ID do usuário
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}