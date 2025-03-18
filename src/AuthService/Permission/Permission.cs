using System.ComponentModel.DataAnnotations;
using AuthService.Permission.Common.Enums;

namespace AuthService.Permission;

/// <summary>
/// Classe que representa uma permissão
/// </summary>
public class Permission(EPermissionAction action, string theme)
{
    /// <summary>
    /// Id da permissão
    /// </summary>
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Tipo da permissão
    /// </summary>
    public EPermissionAction Action { get; private set; } = action;

    /// <summary>
    /// Tema da permissão
    /// </summary>
    public string Theme { get; private set; } = theme;
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;

    public void SetAction(EPermissionAction? action)
    {
        if (action != null && action != Action)
        {
            Action = action.Value;
            UpdatedAt = DateTime.Now;
        }
    }

    public void SetTheme(string? theme)
    {
        if (!string.IsNullOrWhiteSpace(theme) && !theme.Equals(Theme))
        {
            Theme = theme;
            UpdatedAt = DateTime.Now;
        }
    }
}