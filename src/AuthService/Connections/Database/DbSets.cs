using AuthService.Common.User;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Connections.Database;

public partial class AuthDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Permission.Permission> Permissions { get; set; }
    public DbSet<Group.Group> Groups { get; set; }
}