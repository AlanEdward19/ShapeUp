using System.ComponentModel.DataAnnotations;
using AuthService.Authentication.Common.Enums;
using AuthService.Common.User;

namespace AuthService.Group;

/// <summary>
/// Grupo de usuários
/// </summary>
public class Group
{
    /// <summary>
    /// Id do grupo
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Permissões do grupo
    /// </summary>
    public List<Permission.Permission> Permissions { get; set; } = new();
    
    public virtual ICollection<UserGroup> Users { get; set; } = new List<UserGroup>();
    
    public void AddUser(User user, EGroupRole role)
    {
        UserGroup userGroup = new(this, user, role);
        
        Users.Add(userGroup);
    }
    
    public void AddPermission(Permission.Permission permission)
    {
        Permissions.Add(permission);
    }
}