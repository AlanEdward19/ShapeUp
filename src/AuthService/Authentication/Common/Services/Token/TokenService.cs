using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Common.User;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Authentication.Common.Services.Token;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateToken(User user, List<string> permissions)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.ObjectId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("permissions", string.Join(",", permissions))
        };

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}