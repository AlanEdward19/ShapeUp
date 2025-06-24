using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.UserNutrition.CreateUserNutrition;

/// <summary>
/// Handles the creation of user nutrition based on a daily menu.
/// </summary>
/// <param name="userNutritionRepository"></param>
/// <param name="dailyMenuRepository"></param>
public class CreateUserNutritionCommandHandler(IUserNutritionMongoRepository userNutritionRepository, IDailyMenuMongoRepository dailyMenuRepository) : 
    IHandler<UserNutritionDto, CreateUserNutritionCommand>
{
    /// <summary>
    /// Handles the creation of user nutrition based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UserNutritionDto> HandleAsync(CreateUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var builtDailyMenus = await dailyMenuRepository.GetManyByIdsAsync(item.DailyMenuIds, cancellationToken);
        var userNutrition = new UserNutrition(item.NutritionManagerId, builtDailyMenus.ToList());
        userNutrition.SetId();
        userNutrition.SetCreatedBy(ProfileContext.ProfileId);
        
        await userNutritionRepository.InsertUserNutritionAsync(userNutrition);

        return new UserNutritionDto(userNutrition);
    }
}