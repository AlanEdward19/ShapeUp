using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using NutritionService.Meal.CreateMeal;
using NutritionService.Meal.DeleteMeal;
using NutritionService.Meal.EditMeal;
using NutritionService.Meal.GetMealDetails;
using NutritionService.Meal.ListMeals;

namespace NutritionService.Meal;

/// <summary>
/// Módulo para resolver as dependências do serviço de refeições.
/// </summary>
public static class MealModule
{
    /// <summary>
    /// Método de extensão para configurar as dependências relacionadas a refeições.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureMealRelatedDependencies(this IServiceCollection services)
    {
        services.AddRepositories()
            .AddHandlers();
        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMealMongoRepository, MealMongoRepository>();
        return services;
    }
    
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<MealDto, CreateMealCommand>, CreateMealCommandHandler>();
        services.AddScoped<IHandler<bool, EditMealCommand>, EditMealCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteMealCommand>, DeleteMealCommandHandler>();
        services.AddScoped<IHandler<MealDto, GetMealDetailsQuery>, GetMealDetailsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<MealDto>, ListMealsQuery>, ListMealsQueryHandler>();
        return services;
    }
}