using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Common.User.Repository;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using SharedKernel.Utils;

namespace AuthService.Common.Filters;

public class AuthorizationFilter(IConfiguration configuration, IUserRepository userRepository) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var scheme = await ValidateToken(token);
            if (scheme != null)
                context.HttpContext.User = scheme;
            
            else
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
        }
        else
            context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
    }

    private async Task<ClaimsPrincipal?> ValidateToken(string token)
    {
        ClaimsPrincipal principal = null;
        var tokenHandler = new JwtSecurityTokenHandler();

        // AuthenticationService validation parameters
        var key1 = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        var validationParameters1 = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key1)
        };

        // Azure B2C validation parameters
        var validationParameters2 = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["AzureAdB2C:Instance"] + configuration["AzureAdB2C:Domain"] + "/" +
                          configuration["AzureAdB2C:SignUpSignInPolicyId"] + "/v2.0/",
            ValidateAudience = true,
            ValidAudience = configuration["AzureAdB2C:ClientId"],
            ValidateIssuerSigningKey = true
        };

        try
        {
            principal = tokenHandler.ValidateToken(token, validationParameters1, out var validatedToken);
        }
        catch
        {
            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters2, out var validatedToken);
                
                var user = await userRepository.GetByObjectIdAsync(Guid.Parse(principal.GetObjectId()), CancellationToken.None);
                
                ClaimsIdentity newIdentity = new(principal.Identity);
                newIdentity.AddClaim(new Claim("emails", user.Email));
                newIdentity.AddClaim(new Claim("oid", user.ObjectId.ToString()));
                newIdentity.AddClaim(new Claim("permissions",
                    string.Join(",",
                        user.UserGroups
                            .SelectMany(x => x.Group.GroupPermissions
                                .Select(x => x.Permission)))));
                
                principal = new ClaimsPrincipal(newIdentity);
            }
            catch(Exception e)
            {
                return null;
            }
        }
        
        return principal;
    }
}