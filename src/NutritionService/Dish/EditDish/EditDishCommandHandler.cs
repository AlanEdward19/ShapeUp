using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;
using SharedKernel.Utils;

namespace NutritionService.Dish.EditDish;

/// <summary>
/// Handles the command to edit an existing dish.
/// </summary>
/// <param name="dishRepository"></param>
/// <param name="foodRepository"></param>
public class EditDishCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository foodRepository) : IHandler<bool, EditDishCommand>
{
    /// <summary>
    /// Handles the command to edit an existing dish by updating its name and associated food items.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(EditDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await dishRepository.GetDishByIdAsync(item.Id);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");
        
        var foodItems = await foodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);

        existingDish.UpdateInfo(item.Name, foodItems.ToList());
        
        await dishRepository.UpdateDishAsync(existingDish);
        
        return true;
    }
}
