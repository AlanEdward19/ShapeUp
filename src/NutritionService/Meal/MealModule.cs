﻿using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using NutritionService.Meal.CreateMeal;
using NutritionService.Meal.DeleteMeal;
using NutritionService.Meal.EditMeal;
using NutritionService.Meal.GetMealDetails;

namespace NutritionService.Meal;

/// <summary>
/// Módulo para resolver as dependências do serviço de refeições.
/// </summary>
public static class MealModule
{
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
        services.AddScoped<IHandler<Meal, CreateMealCommand>, CreateMealCommandHandler>();
        services.AddScoped<IHandler<Meal, EditMealCommand>, EditMealCommandHandler>();
        services.AddScoped<IHandler<Meal, DeleteMealCommand>, DeleteMealCommandHandler>();
        services.AddScoped<IHandler<Meal, GetMealDetailsQuery>, GetMealDetailsQueryHandler>();
        return services;
    }
}