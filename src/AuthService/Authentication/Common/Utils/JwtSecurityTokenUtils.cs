using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Authentication.Common.Utils;

/// <summary>
///     Classe utilitária para JwtSecurityToken
/// </summary>
public static class JwtSecurityTokenUtils
{
    /// <summary>
    ///     Método para obter o email do usuário
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static string GetEmail(this JwtSecurityToken token)
    {
        return token.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? string.Empty;
    }

    /// <summary>
    ///     Método para obter o id do usuário
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static string GetObjectId(this JwtSecurityToken token)
    {
        return token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value ?? string.Empty;
    }
}