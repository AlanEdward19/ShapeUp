using System.Security.Claims;

namespace SharedKernel.Utils;

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
        return user.FindFirst("email")!.Value;
    }

    /// <summary>
    ///     Método para obter o id do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetObjectId(this ClaimsPrincipal user)
    {
        return user.FindFirst("user_id")!.Value;
    }

    /// <summary>
    ///     Método para obter o primeiro nome do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetFirstName(this ClaimsPrincipal user)
    {
        return user.FindFirst("firstName")!.Value;
    }

    /// <summary>
    ///     Método para obter o sobrenome do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetLastName(this ClaimsPrincipal user)
    {
        return user.FindFirst("lastName")!.Value;
    }
    
    public static string GetFullName(this ClaimsPrincipal user)
    {
        return $"{user.GetFirstName()} {user.GetLastName()}";
    }
    
    /// <summary>
    /// Método para obter o nome de exibição do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetDisplayName(this ClaimsPrincipal user)
    {
        return GetFirstName(user) + " " + GetLastName(user);
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
    ///     Método para obter o CEP do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetPostalCode(this ClaimsPrincipal user)
    {
        return user.FindFirst("postalCode")!.Value;
    }
}