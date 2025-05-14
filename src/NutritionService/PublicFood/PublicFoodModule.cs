using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.PublicFood.CreatePublicFood;
using NutritionService.PublicFood.DeletePublicFood;
using NutritionService.PublicFood.EditPublicFood;
using NutritionService.PublicFood.GetPublicFoodDetails;
using NutritionService.PublicFood.ListUnrevisedPublicFoods;
using NutritionService.UserFood;

namespace NutritionService.PublicFood;

public static class PublicFoodModule
{
    public static IServiceCollection ConfigurePublicFoodRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<Food>, ListUnrevisedPublicFoodsQuery>, ListUnrevisedPublicFoodsQueryHandler>();
        services.AddScoped<IHandler<Food, GetPublicFoodDetailsQuery>, GetPublicFoodDetailsQueryHandler>();
        services.AddScoped<IHandler<Food, CreatePublicFoodCommand>, CreatePublicFoodCommandHandler>();
        services.AddScoped<IHandler<Food, EditPublicFoodCommand>, EditPublicFoodCommandHandler>();
        services.AddScoped<IHandler<Food, DeletePublicFoodCommand>, DeletePublicFoodCommandHandler>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPublicFoodMongoRepository, PublicFoodMongoRepository>();

        return services;
    }
}