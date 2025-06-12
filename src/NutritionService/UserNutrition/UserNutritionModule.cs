using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using NutritionService.UserNutrition.CreateUserNutrition;
using NutritionService.UserNutrition.DeleteUserNutrition;
using NutritionService.UserNutrition.EditUserNutrition;
using NutritionService.UserNutrition.GetUserNutritionDetails;
using NutritionService.UserNutrition.ListUserNutritions;

namespace NutritionService.UserNutrition;

public static class UserNutritionModule
{
    public static IServiceCollection ConfigureUserNutritionRelatedDependencies(this IServiceCollection services)
    {
        services.
            AddRepositories()
            .AddHandlers();
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserNutritionMongoRepository, UserNutritionMongoRepository>();
        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<UserNutrition, CreateUserNutritionCommand>, CreateUserNutritionCommandHandler>();
        services.AddScoped<IHandler<UserNutrition, EditUserNutritionCommand>, EditUserNutritionCommandHandler>();
        services.AddScoped<IHandler<UserNutrition, DeleteUserNutritionCommand>, DeleteUserNutritionCommandHandler>();
        services.AddScoped<IHandler<UserNutrition, GetUserNutritionDetailsQuery>, GetUserNutritionDetailsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<UserNutrition>, ListUserNutritionsQuery>, ListUserNutritionsQueryHandler>();
        return services;
    }
}