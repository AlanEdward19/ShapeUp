﻿using System.ComponentModel.DataAnnotations;
using AuthService.Authentication.Common.Enums;
using AuthService.Group;

namespace AuthService.Common.User;

/// <summary>
/// Classe que representa um usuário
/// </summary>
public class User(Guid objectId, string email)
{
    /// <summary>
    /// Id do usuário
    /// </summary>
    [Key]
    public Guid ObjectId { get; private set; } = objectId;

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
    
    public void AddGroup(Group.Group group, EGroupRole role)
    {
        UserGroup userGroup = new(group, this, role);
        
        UserGroups.Add(userGroup);
        
        UpdatedAt = DateTime.Now;
    }
    
    public void RemoveGroup(Group.Group group)
    {
        UserGroup userGroup = UserGroups.First(ug => ug.GroupId == group.Id);
        
        UserGroups.Remove(userGroup);
        
        UpdatedAt = DateTime.Now;
    }
}
