using System.ComponentModel.DataAnnotations;
using AuthService.Common.User;
using AuthService.Group.Common.Enums;

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
    public Guid Id { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// Permissões do grupo
    /// </summary>
    public virtual ICollection<GroupPermission> GroupPermissions { get; private set; } = new List<GroupPermission>();
    
    public virtual ICollection<UserGroup> Users { get; private set; } = new List<UserGroup>();
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    public void AddUser(User user, EGroupRole role)
    {
        UserGroup userGroup = new(this, user, role);
        Users.Add(userGroup);
        UpdatedAt = DateTime.Now;
    }
    
    public void RemoveUser(User user)
    {
        UserGroup userGroup = Users.First(ug => ug.UserId == user.ObjectId);
        Users.Remove(userGroup);
        UpdatedAt = DateTime.Now;
    }
    
    public void AddPermission(Permission.Permission permission)
    {
        GroupPermission groupPermission = new(this, permission);
        GroupPermissions.Add(groupPermission);
        UpdatedAt = DateTime.Now;
    }
    
    public void RemovePermission(Permission.Permission permission)
    {
        GroupPermission groupPermission = GroupPermissions.First(gp => gp.PermissionId == permission.Id);
        GroupPermissions.Remove(groupPermission);
        UpdatedAt = DateTime.Now;
    }
}