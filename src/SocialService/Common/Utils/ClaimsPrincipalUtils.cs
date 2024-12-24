using System.Security.Claims;

namespace SocialService.Common.Utils;

/// <summary>
///     Classe utilitária para ClaimsPrincipal
/// </summary>
public static class ClaimsPrincipalUtils
{
    /// <summary>
    ///     Método para obter o email do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst("emails")!.Value;
    }

    /// <summary>
    ///     Método para obter o id do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetObjectId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }

    /// <summary>
    ///     Método para obter o primeiro nome do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetFirstName(this ClaimsPrincipal user)
    {
        return user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")!.Value;
    }

    /// <summary>
    ///     Método para obter o sobrenome do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetLastName(this ClaimsPrincipal user)
    {
        return user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")!.Value;
    }

    /// <summary>
    ///     Método para obter a cidade do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetCity(this ClaimsPrincipal user)
    {
        return user.FindFirst("city")!.Value;
    }

    /// <summary>
    ///     Método para obter o país do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetCountry(this ClaimsPrincipal user)
    {
        return user.FindFirst("country")!.Value;
    }

    /// <summary>
    ///     Método para obter o estado do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetState(this ClaimsPrincipal user)
    {
        return user.FindFirst("state")!.Value;
    }
}