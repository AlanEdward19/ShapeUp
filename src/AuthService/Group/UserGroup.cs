using System.ComponentModel.DataAnnotations.Schema;
using AuthService.Authentication.Common.Enums;
using AuthService.Common;
using AuthService.Common.User;

namespace AuthService.Group;

/// <summary>
/// Classe que representa um grupo de usuários
/// </summary>
public class UserGroup
{
    /// <summary>
    /// Id do usuário
    /// </summary>
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Usuário
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Id do grupo do usuário
    /// </summary>
    [ForeignKey(nameof(Group))]
    public Guid GroupId { get; set; }
    
    /// <summary>
    /// Grupo do usuário
    /// </summary>
    public virtual Group Group { get; set; }
    
    public EGroupRole Role { get; set; }

    public UserGroup(Group group, User user, EGroupRole role)
    {
        UserId = user.ObjectId;
        GroupId = group.Id;
        Role = role;
    }
    
    public UserGroup(Guid group, Guid user, EGroupRole role)
    {
        UserId = user;
        GroupId = group;
        Role = role;
    }
}