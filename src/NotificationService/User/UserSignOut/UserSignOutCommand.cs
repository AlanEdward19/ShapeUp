namespace NotificationService.User.UserSignOut;

/// <summary>
/// Comando para remover o dispositivo do usuário
/// </summary>
/// <param name="deviceToken"></param>
public class UserSignOutCommand(string deviceToken)
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