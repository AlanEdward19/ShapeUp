using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Dish.CreateDish;
using NutritionService.Dish.DeleteDish;
using NutritionService.Dish.EditDish;
using NutritionService.Dish.GetDishDetails;
using NutritionService.Dish.ListDishes;

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
        services.AddScoped<IHandler<DishDto, GetDishDetailsQuery>, GetDishDetailsQueryHandler>();
        services.AddScoped<IHandler<DishDto, CreateDishCommand>, CreateDishCommandHandler>();
        services.AddScoped<IHandler<bool, EditDishCommand>, EditDishCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteDishCommand>, DeleteDishCommandHandler>();
        services.AddScoped<IHandler<IEnumerable<DishDto>, ListDishesQuery>, ListDishesQueryHandler>();
        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDishMongoRepository, DishMongoRepository>();
        return services;
    }
}