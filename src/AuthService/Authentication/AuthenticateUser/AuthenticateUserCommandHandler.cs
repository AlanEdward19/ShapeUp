using AuthService.Authentication.Common.Services.AzureAd;
using AuthService.Authentication.Common.Services.Token;
using AuthService.Common.Interfaces;
using AuthService.Common.User;
using AuthService.Common.User.Repository;

namespace AuthService.Authentication.AuthenticateUser;

public class AuthenticateUserCommandHandler(
    IUserRepository userRepository,
    IAzureAdService azureAdService,
    ITokenService tokenService) : IHandler<string, AuthenticateUserCommand>
{
    public async Task<string> HandleAsync(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var azureUser = azureAdService.ValidateToken(command.Token);
        if (azureUser == null) throw new UnauthorizedAccessException();

        var user = await userRepository.GetByObjectIdAsync(azureUser.ObjectId, cancellationToken);
        
        if (user == null)
        {
            user = new User(azureUser.ObjectId, azureUser.Email);
            
            await userRepository.AddAsync(user, cancellationToken);
        }

        var permissions = user.UserGroups.SelectMany(ug => ug.Group.Permissions)
            .Select(p => $"{p.Action.ToString().ToLower()}:{p.Theme}")
            .ToList();

        return tokenService.GenerateToken(user, permissions);
    }
}