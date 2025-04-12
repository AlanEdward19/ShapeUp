using System.ComponentModel.DataAnnotations;
using AuthService.Group;

namespace AuthService.Common.User;

/// <summary>
/// Classe que representa um usuário
/// </summary>
public class UserDto(User user)
{
    /// <summary>
    /// Id do usuário
    /// </summary>
    [Key]
    public string ObjectId { get; private set; } = user.ObjectId;

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; private set; } = user.Email;
}
