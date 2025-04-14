namespace NutritionService.Dish;

public static class DishModule
{
    public static IServiceCollection ConfigureDishRelatedDependencies(this IServiceCollection services)
    {
        services
            .AddRepositories()
            .AddHandlers();

        return services;
    }
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        // services.AddScoped<IHandler<IEnumerable<Dish>, ListUnrevisedDishesQuery>, ListUnrevisedDishesQueryHandler>();
        // services.AddScoped<IHandler<Dish, GetDishDetailsQuery>, GetDishDetailsQueryHandler>();
        // services.AddScoped<IHandler<Dish, CreateDishCommand>, CreateDishCommandHandler>();
        // services.AddScoped<IHandler<Dish, EditDishCommand>, EditDishCommandHandler>();
        // services.AddScoped<IHandler<Dish, ApproveDishCommand>, ApproveDishCommandHandler>();
        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // services.AddScoped<IDishMongoRepository, DishMongoRepository>();
        return services;
    }
}