using System.Security.Claims;

namespace SharedKernel.ValueObjects;

public class UserValueObject
{
    public bool IsValid { get; private set; }
    public List<Claim> Claims { get; private set; }

    public UserValueObject(bool isValid, List<Claim> claims)
    {
        IsValid = isValid;
        Claims = claims;
    }
}