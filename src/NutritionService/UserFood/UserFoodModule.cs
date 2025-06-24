using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.ApproveUserFood;
using NutritionService.UserFood.Common.Repository;
using NutritionService.UserFood.CreateUserFood;
using NutritionService.UserFood.DeleteUserFood;
using NutritionService.UserFood.EditUserFood;
using NutritionService.UserFood.GetUserFoodByBarCode;
using NutritionService.UserFood.GetUserFoodDetails;
using NutritionService.UserFood.InsertPublicFoodsInUserFood;
using NutritionService.UserFood.ListFoods;

namespace NutritionService.UserFood;

/// <summary>
///     Modulo para resolver as dependências relacionadas a comidas
/// </summary>
public static class UserFoodModule
{
    /// <summary>
    ///     Método para resolver as dependências relacionadas a comidas
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureUserFoodRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<IEnumerable<FoodDto>, ListFoodsQuery>, ListFoodsQueryHandler>();
        services.AddScoped<IHandler<FoodDto, GetUserFoodDetailsQuery>, GetUserFoodDetailsQueryHandler>();
        services.AddScoped<IHandler<FoodDto, CreateUserFoodCommand>, CreateUserFoodCommandHandler>();
        services.AddScoped<IHandler<bool, EditUserFoodCommand>, EditUserFoodCommandHandler>();
        services.AddScoped<IHandler<bool, ApproveUserFoodCommand>, ApproveUserFoodCommandHandler>();
        services.AddScoped<IHandler<bool, DeleteUserFoodCommand>, DeleteUserFoodCommandHandler>();
        services
            .AddScoped<IHandler<IEnumerable<FoodDto>, InsertPublicFoodsInUserFoodCommand>,
                InsertPublicFoodsInUserFoodCommandHandler>();
        services.AddScoped<IHandler<FoodDto, GetUserFoodByBarCodeQuery>, GetUserFoodByBarCodeQueryHandler>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserFoodMongoRepository, UserFoodMongoRepository>();

        return services;
    }
}