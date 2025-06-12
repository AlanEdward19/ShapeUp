using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.DailyMenu.CreateDailyMenu;
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
        services.AddScoped<IHandler<DailyMenu, CreateDailyMenuCommand>, CreateDailyMenuCommandHandler>();
        services.AddScoped<IHandler<DailyMenu, DeleteDailyMenuCommand>, DeleteDailyMenuCommandHandler>();
        services.AddScoped<IHandler<DailyMenu, EditDailyMenuCommand>, EditDailyMenuCommandHandler>();
        services.AddScoped<IHandler<DailyMenu, GetDailyMenuDetailsQuery>, GetDailyMenuDetailsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<DailyMenu>, ListDailyMenuQuery>, ListDailyMenuQueryHandler>();
        return services;
    }
}