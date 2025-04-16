using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Dish.CreateDish;
using NutritionService.Dish.DeleteDish;
using NutritionService.Dish.EditDish;
using NutritionService.Dish.GetDishDetails;

namespace NutritionService.Dish;

/// <summary>
/// Módulo para resolver as dependências relacionadas a pratos.
/// </summary>
public static class DishModule
{
    /// <summary>
    /// Método de extensão para configurar as dependências do módulo de pratos.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureDishRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<Dish, GetDishDetailsQuery>, GetDishDetailsQueryHandler>();
        services.AddScoped<IHandler<Dish, CreateDishCommand>, CreateDishCommandHandler>();
        services.AddScoped<IHandler<Dish, EditDishCommand>, EditDishCommandHandler>();
        services.AddScoped<IHandler<Dish, DeleteDishCommand>, DeleteDishCommandHandler>();
        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDishMongoRepository, DishMongoRepository>();
        return services;
    }
}