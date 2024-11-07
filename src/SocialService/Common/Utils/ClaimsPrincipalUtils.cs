﻿using System.Security.Claims;

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

    /// <summary>
    /// Método para obter o primeiro nome do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetFirstName(this ClaimsPrincipal user)
        => user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")!.Value;

    /// <summary>
    /// Método para obter o sobrenome do usuário
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetLastName(this ClaimsPrincipal user)
        => user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")!.Value;
}