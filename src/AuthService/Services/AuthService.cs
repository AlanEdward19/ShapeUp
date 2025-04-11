using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AuthService.Common.User;
using AuthService.Common.User.Repository;
using AuthService.Protos;
using FirebaseAdmin.Auth;
using Grpc.Core;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class AuthService(ILogger<AuthService> logger, IConfiguration configuration) : Protos.AuthService.AuthServiceBase
{
    private string _firebaseIssuer = configuration["Firebase:Issuer"]!;
    private string _projectId = configuration["Firebase:Credentials:project_id"]!;
    private string _firebaseIssuerSigningKey = configuration["Firebase:IssuerSigningKey"]!;
    
    public override async Task<VerifyTokenResponse> VerifyToken(VerifyTokenRequest request, ServerCallContext context)
    {
        var handler = new JwtSecurityTokenHandler();
        
        var certificates = _firebaseIssuerSigningKey.Split("-?-");
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
            ValidIssuer = _firebaseIssuer,
            ValidateAudience = true,
            ValidAudience = _projectId,
            ValidateLifetime = true,
            IssuerSigningKeys = keys
        };

        try
        {
            var claims = handler.ValidateToken(request.Token, validationParameters, out _);
            
            var claimList = claims.Claims.Select(c => new Claim
            {
                Type = c.Type,
                Value = c.Value
            }).ToList();
            
            return new()
            {
                IsValid = true,
                Claims = {claimList}
            };
        }
        catch (Exception)
        {
            return new()
            {
                IsValid = false,
                Claims = {  }
            };
        }
    }
}