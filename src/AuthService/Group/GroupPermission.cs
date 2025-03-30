using System.ComponentModel.DataAnnotations.Schema;
using AuthService.Permission;

namespace AuthService.Group;

/// <summary>
/// Classe que representa a associação entre grupo e permissão
/// </summary>
public class GroupPermission
{
    /// <summary>
    /// Id do grupo
    /// </summary>
    [ForeignKey(nameof(Group))]
    public Guid GroupId { get; set; }
    
    /// <summary>
    /// Grupo
    /// </summary>
    public virtual Group Group { get; set; }

    /// <summary>
    /// Id da permissão
    /// </summary>
    [ForeignKey(nameof(Permission))]
    public Guid PermissionId { get; set; }
    
    /// <summary>
    /// Permissão
    /// </summary>
    public virtual Permission.Permission Permission { get; set; }

    public GroupPermission(Group group, Permission.Permission permission)
    {
        GroupId = group.Id;
        Group = group;
        PermissionId = permission.Id;
        Permission = permission;
    }

    public GroupPermission() { }
}