using NutritionService.Common.Interfaces;
using NutritionService.UserFood.ApproveUserFood;
using NutritionService.UserFood.Common.Repository;
using NutritionService.UserFood.CreateUserFood;
using NutritionService.UserFood.EditUserFood;
using NutritionService.UserFood.GetUserFoodDetails;
using NutritionService.UserFood.ListUnrevisedFoods;

namespace NutritionService.UserFood;

/// <summary>
///     Modulo para resolver as dependências relacionadas a comidas
/// </summary>
public static class UserFoodModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a comidas
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureUserFoodRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<Food>, ListUnrevisedFoodsQuery>, ListUnrevisedFoodsQueryHandler>();
        services.AddScoped<IHandler<Food, GetUserFoodDetailsQuery>, GetUserFoodDetailsQueryHandler>();
        services.AddScoped<IHandler<Food, CreateUserFoodCommand>, CreateUserFoodCommandHandler>();
        services.AddScoped<IHandler<Food, EditUserFoodCommand>, EditUserFoodCommandHandler>();
        services.AddScoped<IHandler<Food, ApproveUserFoodCommand>, ApproveUserFoodCommandHandler>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserFoodMongoRepository, UserFoodMongoRepository>();

        return services;
    }
}