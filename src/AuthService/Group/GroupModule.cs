using AuthService.Common.Interfaces;
using AuthService.Common.User;
using AuthService.Group.AddUserToGroup;
using AuthService.Group.Common.Repository;
using AuthService.Group.CreateGroup;
using AuthService.Group.DeleteGroup;
using AuthService.Group.GetUsersFromGroup;
using AuthService.Group.RemoveUserFromGroup;

namespace AuthService.Group;

public static class GroupModule
{
    public static IServiceCollection ConfigureGroupRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGroupRepository, GroupRepository>();

        return services; 
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<bool, AddUserToGroupCommand>, AddUserToGroupCommandHandler>();
        services.AddScoped<IHandler<bool, CreateGroupCommand>, CreateGroupCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteGroupCommand>, DeleteGroupCommandHandler>();
        services.AddScoped<IHandler<ICollection<UserDto>, GetUsersFromGroupQuery>, GetUsersFromGroupQueryHandler>();
        services.AddScoped<IHandler<bool, RemoveUserFromGroupCommand>, RemoveUserFromGroupCommandHandler>();
        
        return services;
    }
}