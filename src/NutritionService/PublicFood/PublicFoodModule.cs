using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.ApprovePublicFood;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.PublicFood.CreatePublicFood;
using NutritionService.PublicFood.DeletePublicFood;
using NutritionService.PublicFood.EditPublicFood;
using NutritionService.PublicFood.GetPublicFoodByBarCode;
using NutritionService.PublicFood.GetPublicFoodDetails;
using NutritionService.PublicFood.ListCreatedByUserPublicFoods;
using NutritionService.PublicFood.ListPublicFoods;
using NutritionService.PublicFood.ListRevisedPublicFoods;
using NutritionService.PublicFood.ListUnrevisedPublicFoods;
using NutritionService.PublicFood.ListUsedByUserPublicFoods;
using NutritionService.UserFood;

namespace NutritionService.PublicFood;

/// <summary>
///     Modulo para resolver as dependências relacionadas a comidas públicas
/// </summary>
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
        services.AddScoped<IHandler<IEnumerable<FoodDto>, ListUnrevisedPublicFoodsQuery>, ListUnrevisedPublicFoodsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<FoodDto>, ListPublicFoodsQuery>, ListPublicFoodsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<FoodDto>, ListCreatedByUserPublicFoodsQuery>, ListCreatedByUserPublicFoodsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<FoodDto>, ListUsedByUserPublicFoodsQuery>, ListUsedByUserPublicFoodsQueryHandler>();
        services.AddScoped<IHandler<IEnumerable<FoodDto>, ListRevisedPublicFoodsQuery>, ListRevisedPublicFoodsQueryHandler>();
        services.AddScoped<IHandler<FoodDto, GetPublicFoodDetailsQuery>, GetPublicFoodDetailsQueryHandler>();
        services.AddScoped<IHandler<FoodDto, CreatePublicFoodCommand>, CreatePublicFoodCommandHandler>();
        services.AddScoped<IHandler<bool, EditPublicFoodCommand>, EditPublicFoodCommandHandler>();
        services.AddScoped<IHandler<bool, DeletePublicFoodCommand>, DeletePublicFoodCommandHandler>();
        services.AddScoped<IHandler<bool, ApprovePublicFoodCommand>, ApprovePublicFoodCommandHandler>();
        services.AddScoped<IHandler<FoodDto, GetPublicFoodByBarCodeQuery>, GetPublicFoodByBarCodeQueryHandler>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPublicFoodMongoRepository, PublicFoodMongoRepository>();

        return services;
    }
}