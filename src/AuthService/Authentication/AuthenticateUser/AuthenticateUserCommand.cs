namespace AuthService.Authentication.AuthenticateUser;

public class AuthenticateUserCommand(string token)
{
    public string Token { get; private set; } = token;
}