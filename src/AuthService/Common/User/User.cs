using System.ComponentModel.DataAnnotations;
using AuthService.Group;

namespace AuthService.Common.User;

/// <summary>
/// Classe que representa um usuário
/// </summary>
public class User(string objectId, string email)
{
    /// <summary>
    /// Id do usuário
    /// </summary>
    [Key]
    public string ObjectId { get; private set; } = objectId;

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; private set; } = email;
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;

    /// <summary>
    /// Grupos do usuário
    /// </summary>
    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}
