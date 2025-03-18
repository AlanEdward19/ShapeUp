namespace AuthService.Authentication.Common.Services.AzureAd;

public class AzureUserValueObject(string objectId, string email)
{
    public Guid ObjectId { get; private set; } =
        string.IsNullOrWhiteSpace(objectId) ? Guid.Empty : Guid.Parse(objectId);
    
    public string Email { get; private set; } = email;
}