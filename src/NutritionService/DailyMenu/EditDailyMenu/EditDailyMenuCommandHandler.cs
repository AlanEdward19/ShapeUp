using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.EditDailyMenu;

public class EditDailyMenuCommandHandler(IDailyMenuMongoRepository dailyMenuRepository, IMealMongoRepository mealRepository):
    IHandler<DailyMenu, EditDailyMenuCommand>
{
    public async Task<DailyMenu> HandleAsync(EditDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var existingDailyMenu = await dailyMenuRepository.GetDailyMenuDetailsAsync(item.Id);
        
        if (existingDailyMenu == null)
            throw new NotFoundException(item.Id);
        
        var builtMeal = await mealRepository.GetManyMealsByIdsAsync(item.MealIds, cancellationToken);
        existingDailyMenu.UpdateInfo(item.DayOfWeek, builtMeal.ToList());
        
        await dailyMenuRepository.UpdateDailyMenuAsync(existingDailyMenu);

        return existingDailyMenu;
    }
}