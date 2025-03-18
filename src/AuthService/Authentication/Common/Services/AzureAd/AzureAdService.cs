using System.IdentityModel.Tokens.Jwt;
using AuthService.Authentication.Common.Utils;

namespace AuthService.Authentication.Common.Services.AzureAd;

public class AzureAdService : IAzureAdService
{
    public AzureUserValueObject ValidateToken(string azureToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(azureToken);

        var objectId = jwtToken.GetObjectId();
        var email = jwtToken.GetEmail();

        if (string.IsNullOrWhiteSpace(objectId)) throw new UnauthorizedAccessException("Invalid token");

        return new AzureUserValueObject(objectId, email);
    }
}