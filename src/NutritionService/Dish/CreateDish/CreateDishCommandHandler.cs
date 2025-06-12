using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Dish.CreateDish;

public class CreateDishCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository userFoodRepository) : IHandler<Dish, CreateDishCommand>
{
    public async Task<Dish> HandleAsync(CreateDishCommand item, CancellationToken cancellationToken)
    {
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);
        
        var dish = new Dish(item.Name, builtFoods.ToList());
        dish.SetId();
        dish.SetCreatedBy(ProfileContext.ProfileId);
        
        await dishRepository.InsertDishAsync(dish);

        return dish;
    }
}