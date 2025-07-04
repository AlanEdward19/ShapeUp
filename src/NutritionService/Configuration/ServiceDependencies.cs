﻿using NutritionService.Connections;
using NutritionService.DailyMenu;
using NutritionService.Dish;
using NutritionService.Meal;
using NutritionService.PublicFood;
using NutritionService.UserFood;
using NutritionService.UserNutrition;

namespace NutritionService.Configuration;

/// <summary>
///     Classe para resolver as dependências de serviços
/// </summary>
public static class ServiceDependencies
{
    /// <summary>
    ///     Método para resolver as dependências de serviços
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection SolveServiceDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureConnections(configuration)
            .ConfigurePublicFoodRelatedDependencies()
            .ConfigureUserFoodRelatedDependencies()
            .ConfigureDishRelatedDependencies()
            .ConfigureMealRelatedDependencies()
            .ConfigureDailyMenuRelatedDependencies()
            .ConfigureUserNutritionRelatedDependencies();
        
        return services;
    }
}