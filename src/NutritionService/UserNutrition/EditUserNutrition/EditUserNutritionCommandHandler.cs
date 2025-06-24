using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.EditUserNutrition;

/// <summary>
/// Handles the editing of user nutrition details.
/// </summary>
/// <param name="userNutritionRepository"></param>
/// <param name="dailyMenuRepository"></param>
public class EditUserNutritionCommandHandler(IUserNutritionMongoRepository userNutritionRepository, IDailyMenuMongoRepository dailyMenuRepository): 
    IHandler<bool, EditUserNutritionCommand>
{
    /// <summary>
    /// Handles the editing of user nutrition details based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(EditUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var existingUserNutrition = await userNutritionRepository.GetUserNutritionDetailsAsync(item.Id);
        
        if (existingUserNutrition == null)
            throw new NotFoundException(item.Id!);

        var builtDailyMenus = await dailyMenuRepository.GetManyByIdsAsync(item.DailyMenus, cancellationToken);
        
        existingUserNutrition.UpdateInfo(item.NutritionManagerId, builtDailyMenus.ToList());
        
        await userNutritionRepository.UpdateUserNutritionAsync(existingUserNutrition);

        return true;
    }
}