using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.EditMeal;

/// <summary>
/// Handles the editing of an existing meal.
/// </summary>
/// <param name="repository"></param>
/// <param name="userFoodRepository"></param>
/// <param name="dishRepository"></param>
public class EditMealCommandHandler(IMealMongoRepository repository, IUserFoodMongoRepository userFoodRepository, IDishMongoRepository dishRepository) : IHandler<bool, EditMealCommand>
{
    /// <summary>
    /// Handles the command to edit an existing meal by updating its type, name, dishes, and foods.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(EditMealCommand item, CancellationToken cancellationToken)
    {
        var existingMeal = await repository.GetMealByIdAsync(item.Id);
        
        if (existingMeal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");
        
        var builtDishes = await dishRepository.GetManyByIdsAsync(item.DishIds, cancellationToken);
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);

        existingMeal.UpdateInfo(item.Type, item.Name, builtDishes.ToList(), builtFoods.ToList());
        
        await repository.UpdateMealAsync(existingMeal);

        return true;
    }
}