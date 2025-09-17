using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.DailyMenu.CreateDailyMenuForDifferentUser;
using NutritionService.DailyMenu.CreateDailyMenuForSameUser;
using NutritionService.DailyMenu.DeleteDailyMenu;
using NutritionService.DailyMenu.EditDailyMenu;
using NutritionService.DailyMenu.GetDailyMenuDetails;
using NutritionService.DailyMenu.ListDailyMenus;

namespace NutritionService.DailyMenu;

/// <summary>
/// Módulo para resolver as dependências do Cardápio Diário.
/// </summary>
public static class DailyMenuModule
{
    /// <summary>
    /// Método de extensão para configurar as dependências do módulo de Cardápio Diário.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureDailyMenuRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDailyMenuMongoRepository, DailyMenuMongoRepository>();
        return services;
    }
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<DailyMenuDto, CreateDailyMenuForSameUserCommand>, CreateDailyMenuForSameUserCommandHandler>();
        services.AddScoped<IHandler<DailyMenuDto, CreateDailyMenuForDifferentUserCommand>, CreateDailyMenuForDifferentUserCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteDailyMenuCommand>, DeleteDailyMenuCommandHandler>();
        services.AddScoped<IHandler<bool, EditDailyMenuCommand>, EditDailyMenuCommandHandler>();
        services.AddScoped<IHandler<DailyMenuDto, GetDailyMenuDetailsQuery>, GetDailyMenuDetailsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<DailyMenuDto>, ListDailyMenuQuery>, ListDailyMenuQueryHandler>();
        return services;
    }
}