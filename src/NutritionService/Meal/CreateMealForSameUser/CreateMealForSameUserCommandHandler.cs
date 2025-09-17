using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Meal.CreateMealForSameUser;

/// <summary>
/// Handles the creation of a new meal.
/// </summary>
/// <param name="mealRepository"></param>
/// <param name="userFoodRepository"></param>
/// <param name="dishRepository"></param>
public class CreateMealForSameUserCommandHandler(IMealMongoRepository mealRepository, IUserFoodMongoRepository userFoodRepository, IDishMongoRepository dishRepository) : IHandler<MealDto, CreateMealForSameUserCommand>
{
    /// <summary>
    /// Handles the creation of a new meal based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<MealDto> HandleAsync(CreateMealForSameUserCommand item, CancellationToken cancellationToken)
    {
        var builtDishes = await dishRepository.GetManyByIdsAsync(item.DishIds, cancellationToken);
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);
        
        var meal = new Meal(item.Type, item.Name, builtDishes.ToList(), builtFoods.ToList());
        meal.SetCreatedBy(ProfileContext.ProfileId);
        meal.SetId();
        meal.SetUserId(ProfileContext.ProfileId);
        
        await mealRepository.InsertMealAsync(meal);

        return new MealDto(meal);
    }
}