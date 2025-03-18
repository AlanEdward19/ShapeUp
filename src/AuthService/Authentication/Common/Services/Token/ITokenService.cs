using AuthService.Common.User;

namespace AuthService.Authentication.Common.Services.Token;

public interface ITokenService
{
    string GenerateToken(User user, List<string> permissions);
}