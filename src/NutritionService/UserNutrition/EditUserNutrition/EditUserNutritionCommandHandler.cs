using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.EditUserNutrition;

public class EditUserNutritionCommandHandler(IUserNutritionMongoRepository UserNutritionRepository, IDailyMenuMongoRepository DailyMenuRepository): 
    IHandler<UserNutrition, EditUserNutritionCommand>
{
    public async Task<UserNutrition> HandleAsync(EditUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var existingUserNutrition = await UserNutritionRepository.GetUserNutritionDetailsAsync(item.Id);
        
        if (existingUserNutrition == null)
            throw new NotFoundException(item.Id);

        var builtDailyMenus = await DailyMenuRepository.GetManyByIdsAsync(item.DailyMenus, cancellationToken);
        
        existingUserNutrition.UpdateInfo(item.NutritionManagerId, builtDailyMenus.ToList());
        
        await UserNutritionRepository.UpdateUserNutritionAsync(existingUserNutrition);

        return existingUserNutrition;
    }
}