using System.ComponentModel.DataAnnotations;
using SharedKernel.Enums;

namespace AuthService.Permission;

/// <summary>
/// Classe que representa uma permissão
/// </summary>
public class PermissionDto(Permission permission)
{
    /// <summary>
    /// Id da permissão
    /// </summary>
    [Key]
    public Guid Id { get; init; } = permission.Id;

    /// <summary>
    /// Tipo da permissão
    /// </summary>
    public EPermissionAction Action { get; init; } = permission.Action;

    /// <summary>
    /// Tema da permissão
    /// </summary>
    public string Theme { get; init; } = permission.Theme;
}