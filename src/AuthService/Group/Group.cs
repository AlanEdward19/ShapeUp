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
    /// Nome do grupo
    /// </summary>
    [MaxLength(100)]
    [MinLength(5)]
    public string Name { get; private set; }
    
    /// <summary>
    /// Descrição do grupo
    /// </summary>
    [MaxLength(255)]
    [MinLength(10)]
    public string Description { get; private set; }
    
    /// <summary>
    /// Permissões do grupo
    /// </summary>
    public virtual ICollection<GroupPermission> GroupPermissions { get; private set; } = new List<GroupPermission>();
    
    /// <summary>
    /// Usuários do grupo
    /// </summary>
    public virtual ICollection<UserGroup> Users { get; private set; } = new List<UserGroup>();
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    
    public void AddUser(User user, EGroupRole role)
    {
        UserGroup userGroup = new(this, user, role);
        Users.Add(userGroup);
        UpdatedAt = DateTime.Now;
    }

    public void ChangeUserRole(User user, EGroupRole role)
    {
        Users.First(x => x.User == user).Role = role;
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

    /// <summary>
    /// Construtor para EF Core
    /// </summary>
    public Group()
    {
    }

    /// <summary>
    /// Construtor para criação de grupo
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public Group(string name, string description)
    {
        Name = name;
        Description = description;
    }
}