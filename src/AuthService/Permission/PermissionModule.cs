using AuthService.Common.Interfaces;
using AuthService.Permission.Common.Repository;
using AuthService.Permission.CreatePermission;
using AuthService.Permission.DeletePermission;
using AuthService.Permission.GetGroupPermissions;
using AuthService.Permission.GetUserPermissions;
using AuthService.Permission.GrantGroupPermission;
using AuthService.Permission.GrantUserPermission;
using AuthService.Permission.RemoveGroupPermission;
using AuthService.Permission.RemoveUserPermission;
using AuthService.Permission.UpdatePermission;

namespace AuthService.Permission;

public static class PermissionModule
{
    public static IServiceCollection ConfigurePermissionRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        return services; 
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<PermissionDto, CreatePermissionCommand>, CreatePermissionCommandHandler>();
        services.AddScoped<IHandler<bool, DeletePermissionCommand>, DeletePermissionCommandHandler>();
        services.AddScoped<IHandler<ICollection<PermissionDto>, GetGroupPermissionsQuery>, GetGroupPermissionsQueryHandler>();
        services.AddScoped<IHandler<UserPermissionsDto, GetUserPermissionsQuery>, GetUserPermissionsQueryHandler>();
        services.AddScoped<IHandler<bool, GrantGroupPermissionCommand>, GrantGroupPermissionCommandHandler>();
        services.AddScoped<IHandler<bool, GrantUserPermissionCommand>, GrantUserPermissionCommandHandler>();
        services.AddScoped<IHandler<PermissionDto, UpdatePermissionCommand>, UpdatePermissionCommandHandler>();
        services.AddScoped<IHandler<bool, RemoveUserPermissionCommand>, RemoveUserPermissionCommandHandler>();
        services.AddScoped<IHandler<bool, RemoveGroupPermissionCommand>, RemoveGroupPermissionCommandHandler>();
        
        return services;
    }
}