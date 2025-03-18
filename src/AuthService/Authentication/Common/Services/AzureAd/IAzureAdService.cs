namespace AuthService.Authentication.Common.Services.AzureAd;

public interface IAzureAdService
{
    AzureUserValueObject ValidateToken(string azureToken);
}