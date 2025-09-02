using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SharedKernel.Filters;

public class TokenValidatorFilter: Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;
        var token = httpContext.Request.Headers["Authorization"].First()?.Split(" ")[1] ?? "";
        
        IConfiguration configuration = httpContext.RequestServices.GetRequiredService<IConfiguration>();
        
        string firebaseIssuer = configuration["Firebase:Issuer"]!;
        string projectId = configuration["Firebase:Credentials:project_id"]!;
        string firebaseIssuerSigningKey = configuration["Firebase:IssuerSigningKey"]!;
        
        if(string.IsNullOrWhiteSpace(token))
            throw new UnauthorizedAccessException("Token não informado");
        
        var handler = new JwtSecurityTokenHandler();
        
        var certificates = firebaseIssuerSigningKey.Split("-?-");
        List<SecurityKey> keys = [];
        
        foreach (var cert in certificates)
        {
            var x509Cert = new X509Certificate2(Encoding.UTF8.GetBytes(cert));
            var rsa = x509Cert.GetRSAPublicKey();
            keys.Add(new RsaSecurityKey(rsa));
        }

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = firebaseIssuer,
            ValidateAudience = true,
            ValidAudience = projectId,
            ValidateLifetime = true,
            IssuerSigningKeys = keys
        };

        try
        {
            handler.ValidateToken(token, validationParameters, out _);

            var securityToken  = handler.ReadJwtToken(token);
            
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "Firebase"));
        }
        catch(Exception ex)
        {
            throw new UnauthorizedAccessException("Usuário não está autenticado");
        }
    }
}