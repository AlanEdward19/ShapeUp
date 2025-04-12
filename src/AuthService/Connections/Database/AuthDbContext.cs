using AuthService.Group;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;

namespace AuthService.Connections.Database;

public partial class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>().HasKey(ug => new { ug.UserId, ug.GroupId });
        modelBuilder.Entity<GroupPermission>().HasKey(gp => new { gp.GroupId, gp.PermissionId });
        
        
        modelBuilder.Entity<Permission.Permission>().HasData(
            new(Guid.Parse("0C9C9154-2479-406C-8BD2-E15A22C8B31E"), EPermissionAction.Read, "permission", new DateTime(2025, 03, 30), new DateTime(2025, 03, 30)),
            new(Guid.Parse("D3856D63-8C6D-41C7-BFB0-4A327662A6DC"), EPermissionAction.Delete, "permission", new DateTime(2025, 03, 30), new DateTime(2025, 03, 30)),
            new(Guid.Parse("E0DCBF9B-1D24-413A-8839-5D70F9ACE22A"), EPermissionAction.Update, "permission", new DateTime(2025, 03, 30), new DateTime(2025, 03, 30)),
            new(Guid.Parse("A86B7651-2AEF-4F55-9C85-7B1F6D486F14"), EPermissionAction.Write, "permission", new DateTime(2025, 03, 30), new DateTime(2025, 03, 30))
        );
    }
}