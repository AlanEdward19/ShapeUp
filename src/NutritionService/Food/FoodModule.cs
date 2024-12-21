using NutritionService.Common.Interfaces;
using NutritionService.Food.ApproveFood;
using NutritionService.Food.Common.Repository;
using NutritionService.Food.CreateFood;
using NutritionService.Food.EditFood;
using NutritionService.Food.GetFoodDetails;
using NutritionService.Food.ListNonRevisedFoods;

namespace NutritionService.Food;

/// <summary>
///     Modulo para resolver as dependências relacionadas a comidas
/// </summary>
public static class FoodModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a comidas
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureFoodRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<Food>, ListUnrevisedFoodsQuery>, ListUnrevisedFoodsQueryHandler>();
        services.AddScoped<IHandler<Food, GetFoodDetailsQuery>, GetFoodDetailsQueryHandler>();
        services.AddScoped<IHandler<Food, CreateFoodCommand>, CreateFoodCommandHandler>();
        services.AddScoped<IHandler<Food, EditFoodCommand>, EditFoodCommandHandler>();
        services.AddScoped<IHandler<Food, ApproveFoodCommand>, ApproveFoodCommandHandler>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFoodMongoRepository, FoodMongoRepository>();

        return services;
    }
}