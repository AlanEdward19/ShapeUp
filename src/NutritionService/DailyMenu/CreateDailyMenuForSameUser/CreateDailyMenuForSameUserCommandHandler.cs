using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Meal.Common;
using SharedKernel.Utils;

namespace NutritionService.DailyMenu.CreateDailyMenuForSameUser;

/// <summary>
/// Handles the creation of a daily menu.
/// </summary>
/// <param name="dailyMenuRepository"></param>
/// <param name="mealRepository"></param>
public class CreateDailyMenuForSameUserCommandHandler(IDailyMenuMongoRepository dailyMenuRepository, IMealMongoRepository mealRepository) : 
    IHandler<DailyMenuDto, CreateDailyMenuForSameUserCommand>
{
    /// <summary>
    /// Handles the creation of a daily menu.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DailyMenuDto> HandleAsync(CreateDailyMenuForSameUserCommand item, CancellationToken cancellationToken)
    {
        var builtMeal = await mealRepository.GetManyMealsByIdsAsync(item.MealIds, cancellationToken);
        var dailyMenu = new DailyMenu(item.DayOfWeek, builtMeal.ToList());
        dailyMenu.SetId();
        dailyMenu.SetCreatedBy(ProfileContext.ProfileId);
        dailyMenu.SetUserId(ProfileContext.ProfileId);
        
        await dailyMenuRepository.InsertDailyMenuAsync(dailyMenu);

        return new DailyMenuDto(dailyMenu);
    }
}