using System.ComponentModel.DataAnnotations;
using SharedKernel.Enums;

namespace AuthService.Permission;

/// <summary>
/// Classe que representa uma permissão
/// </summary>
public class Permission(Guid id, EPermissionAction action, string theme, DateTime createdAt, DateTime updatedAt)
{
    /// <summary>
    /// Id da permissão
    /// </summary>
    [Key]
    public Guid Id { get; init; } = id;

    /// <summary>
    /// Tipo da permissão
    /// </summary>
    public EPermissionAction Action { get; private set; } = action;

    /// <summary>
    /// Tema da permissão
    /// </summary>
    public string Theme { get; private set; } = theme;

    public DateTime CreatedAt { get; init; } = createdAt;

    public DateTime UpdatedAt { get; private set; } = updatedAt;

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