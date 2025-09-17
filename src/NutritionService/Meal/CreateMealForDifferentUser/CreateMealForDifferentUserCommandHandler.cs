using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Meal.CreateMealForDifferentUser;

public class CreateMealForDifferentUserCommandHandler(IMealMongoRepository mealRepository, IUserFoodMongoRepository userFoodRepository, IDishMongoRepository dishRepository) : IHandler<MealDto, CreateMealForDifferentUserCommand>
{
    /// <summary>
    /// Handles the creation of a new meal based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<MealDto> HandleAsync(CreateMealForDifferentUserCommand item, CancellationToken cancellationToken)
    {
        var builtDishes = await dishRepository.GetManyByIdsAsync(item.DishIds, cancellationToken);
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);
        
        var meal = new Meal(item.Type, item.Name, builtDishes.ToList(), builtFoods.ToList());
        meal.SetCreatedBy(ProfileContext.ProfileId);
        meal.SetId();
        meal.SetUserId(item.UserId);
        
        await mealRepository.InsertMealAsync(meal);

        return new MealDto(meal);
    }
}