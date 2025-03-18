using System.ComponentModel.DataAnnotations;
using AuthService.Permission.Common.Enums;

namespace AuthService.Permission;

/// <summary>
/// Classe que representa uma permissão
/// </summary>
public class Permission
{
    /// <summary>
    /// Id da permissão
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Tipo da permissão
    /// </summary>
    public EPermissionAction Action { get; set; }
    
    /// <summary>
    /// Tema da permissão
    /// </summary>
    public string Theme { get; set; }
}