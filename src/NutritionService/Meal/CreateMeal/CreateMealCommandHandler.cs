using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Meal.CreateMeal;

public class CreateMealCommandHandler(IMealMongoRepository mealRepository, IUserFoodMongoRepository userFoodRepository, IDishMongoRepository dishRepository) : IHandler<Meal, CreateMealCommand>
{
    public async Task<Meal> HandleAsync(CreateMealCommand item, CancellationToken cancellationToken)
    {
        var builtDishes = await dishRepository.GetManyByIdsAsync(item.DishIds, cancellationToken);
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);
        
        var meal = new Meal(item.Type, item.Name, builtDishes.ToList(), builtFoods.ToList());
        meal.SetCreatedBy(ProfileContext.ProfileId);
        meal.SetId();
        
        await mealRepository.InsertMealAsync(meal);

        return meal;
    }
}