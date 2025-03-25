using AuthService.Group;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Connections.Database;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });
        modelBuilder.Entity<GroupPermission>().HasKey(gp => new { gp.GroupId, gp.PermissionId });
    }
}