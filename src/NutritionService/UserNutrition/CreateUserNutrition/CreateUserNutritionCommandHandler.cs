using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.UserNutrition.CreateUserNutrition;

public class CreateUserNutritionCommandHandler(IUserNutritionMongoRepository userNutritionRepository, IDailyMenuMongoRepository dailyMenuRepository) : 
    IHandler<UserNutrition, CreateUserNutritionCommand>
{
    public async Task<UserNutrition> HandleAsync(CreateUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var builtDailyMenus = await dailyMenuRepository.GetManyByIdsAsync(item.DailyMenuIds, cancellationToken);
        var userNutrition = new UserNutrition(item.NutritionManagerId, builtDailyMenus.ToList());
        userNutrition.SetId();
        userNutrition.SetCreatedBy(ProfileContext.ProfileId);
        
        await userNutritionRepository.InsertUserNutritionAsync(userNutrition);

        return userNutrition;
    }
}