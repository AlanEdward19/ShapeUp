using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.EditDailyMenu;

/// <summary>
/// Handles the editing of an existing daily menu.
/// </summary>
/// <param name="dailyMenuRepository"></param>
/// <param name="mealRepository"></param>
public class EditDailyMenuCommandHandler(IDailyMenuMongoRepository dailyMenuRepository, IMealMongoRepository mealRepository):
    IHandler<bool, EditDailyMenuCommand>
{
    /// <summary>
    /// Handles the editing of an existing daily menu based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(EditDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var existingDailyMenu = await dailyMenuRepository.GetDailyMenuDetailsAsync(item.Id);
        
        if (existingDailyMenu == null)
            throw new NotFoundException(item.Id!);
        
        var builtMeal = await mealRepository.GetManyMealsByIdsAsync(item.MealIds, cancellationToken);
        existingDailyMenu.UpdateInfo(item.DayOfWeek, builtMeal.ToList());
        
        await dailyMenuRepository.UpdateDailyMenuAsync(existingDailyMenu);

        return true;
    }
}