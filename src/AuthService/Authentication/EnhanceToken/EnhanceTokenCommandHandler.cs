using System.Text.Json;
using AuthService.Common;
using AuthService.Common.Interfaces;
using AuthService.Common.User.Repository;
using FirebaseAdmin.Auth;
using SharedKernel.Utils;

namespace AuthService.Authentication.EnhanceToken;

public class EnhanceTokenCommandHandler(IUserRepository repository) : IHandler<bool, EnhanceTokenCommand>
{
    public async Task<bool> HandleAsync(EnhanceTokenCommand command, CancellationToken cancellationToken)
    {
        Dictionary<string, object> claims = command.Scopes
            .ToDictionary(x => x.Key, x => ConvertJsonElement(x.Value));

        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(ProfileContext.ProfileId, claims, cancellationToken);
        
        var userFirebase = await FirebaseAuth.DefaultInstance.GetUserAsync(ProfileContext.ProfileId, cancellationToken);
        var userDatabase = await repository.GetByObjectIdAsync(ProfileContext.ProfileId, cancellationToken);
        
        if (userDatabase == null)
        {
            userDatabase = new(ProfileContext.ProfileId, userFirebase.Email);
                
            await repository.AddAsync(userDatabase, cancellationToken);
        }

        return true;
    }
    
    private object ConvertJsonElement(object value)
    {
        if (value is JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.String:
                    return jsonElement.GetString();
                case JsonValueKind.Number:
                    return jsonElement.GetDouble(); // or GetInt32(), GetInt64(), etc.
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return jsonElement.GetBoolean();
                case JsonValueKind.Object:
                case JsonValueKind.Array:
                    return jsonElement.GetRawText(); // or handle nested objects/arrays as needed
                default:
                    return jsonElement.GetRawText();
            }
        }
        return value;
    }
}