using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.UserFood.InsertPublicFoodsInUserFood;

/// <summary>
/// Command handler to insert public foods into user food collection.
/// </summary>
/// <param name="userFoodMongoRepository"></param>
/// <param name="publicFoodMongoRepository"></param>
public class InsertPublicFoodsInUserFoodCommandHandler(IUserFoodMongoRepository userFoodMongoRepository, IPublicFoodMongoRepository publicFoodMongoRepository)
    : IHandler<IEnumerable<FoodDto>, InsertPublicFoodsInUserFoodCommand>
{
    /// <summary>
    /// Handles the insertion of public foods into the user's food collection.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FoodDto>> HandleAsync(InsertPublicFoodsInUserFoodCommand item, CancellationToken cancellationToken)
    {
        var publicFoods = await publicFoodMongoRepository.GetManyByIdsAsync(item.PublicFoodIds, cancellationToken);
        var existingUserFoods = await userFoodMongoRepository.GetAllByUserIdAsync(ProfileContext.ProfileId, cancellationToken);
        var existingIds = existingUserFoods.Select(f => f.Id).ToHashSet();

        var tasks = publicFoods
            .Where(f => !existingIds.Contains(f.Id))
            .Select(async f =>
            {
                var cloned = f.Clone(); 
                cloned.SetId();
                cloned.SetCreatedBy(ProfileContext.ProfileId);
                await userFoodMongoRepository.InsertUserFoodAsync(cloned);
                return new FoodDto(cloned);
            });
        
        return await Task.WhenAll(tasks);
    }
}