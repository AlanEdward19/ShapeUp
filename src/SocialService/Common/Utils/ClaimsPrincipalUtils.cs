using System.Security.Claims;

namespace SocialService.Common.Utils;

/// <summary>
/// Classe utilitária para ClaimsPrincipal
/// </summary>
public static class ClaimsPrincipalUtils
{
    /// <summary>
    /// Método para obter o email do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetEmail(this ClaimsPrincipal user)
        => user.FindFirst("emails")!.Value;
    
    /// <summary>
    /// Método para obter o id do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetObjectId(this ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
}