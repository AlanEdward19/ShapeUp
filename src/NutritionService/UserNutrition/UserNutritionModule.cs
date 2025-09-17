using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using NutritionService.UserNutrition.CreateUserNutrition;
using NutritionService.UserNutrition.DeleteUserNutrition;
using NutritionService.UserNutrition.EditUserNutrition;
using NutritionService.UserNutrition.GetUserNutritionDetails;
using NutritionService.UserNutrition.ListManagedUserNutritions;

namespace NutritionService.UserNutrition;

/// <summary>
/// Módulo para resolver as dependências relacionadas à nutrição do usuário.
/// </summary>
public static class UserNutritionModule
{
    /// <summary>
    /// Método de extensão para configurar as dependências do módulo de nutrição do usuário.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
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
        services.AddScoped<IHandler<UserNutritionDto, CreateUserNutritionCommand>, CreateUserNutritionCommandHandler>();
        services.AddScoped<IHandler<bool, EditUserNutritionCommand>, EditUserNutritionCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteUserNutritionCommand>, DeleteUserNutritionCommandHandler>();
        services.AddScoped<IHandler<UserNutritionDto, GetUserNutritionDetailsQuery>, GetUserNutritionDetailsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<UserNutritionDto>, ListManagedUserNutritionsQuery>, ListManagedUserNutritionsQueryHandler>();
        return services;
    }
}