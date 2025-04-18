using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.EditUserNutrition;

public class EditUserNutritionCommandHandler(IUserNutritionMongoRepository repository) : 
    IHandler<UserNutrition, EditUserNutritionCommand>
{
    public async Task<UserNutrition> HandleAsync(EditUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var existingUserNutrition = await repository.GetUserNutritionDetailsAsync(item.Id);
        
        if (existingUserNutrition == null)
            throw new NotFoundException(item.Id);

        existingUserNutrition.UpdateInfo(item.NutritionManagerId, item.DailyMenus);
        
        await repository.UpdateUserNutritionAsync(existingUserNutrition);

        return existingUserNutrition;
    }
}