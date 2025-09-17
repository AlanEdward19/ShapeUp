using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Meal.Common;
using SharedKernel.Utils;

namespace NutritionService.DailyMenu.CreateDailyMenuForDifferentUser;

public class CreateDailyMenuForDifferentUserCommandHandler(
    IDailyMenuMongoRepository dailyMenuRepository,
    IMealMongoRepository mealRepository) :
    IHandler<DailyMenuDto, CreateDailyMenuForDifferentUserCommand>
{
    /// <summary>
    /// Handles the creation of a daily menu.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DailyMenuDto> HandleAsync(CreateDailyMenuForDifferentUserCommand item,
        CancellationToken cancellationToken)
    {
        var builtMeal = await mealRepository.GetManyMealsByIdsAsync(item.MealIds, cancellationToken);
        var dailyMenu = new DailyMenu(item.DayOfWeek, builtMeal.ToList());
        dailyMenu.SetId();
        dailyMenu.SetCreatedBy(ProfileContext.ProfileId);
        dailyMenu.SetUserId(item.UserId);

        await dailyMenuRepository.InsertDailyMenuAsync(dailyMenu);

        return new DailyMenuDto(dailyMenu);
    }
}