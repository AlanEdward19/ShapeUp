using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.UserFood.InsertPublicFoodsInUserFood;

public class InsertPublicFoodsInUserFoodCommandHandler(IUserFoodMongoRepository userFoodMongoRepository, IPublicFoodMongoRepository publicFoodMongoRepository)
    : IHandler<IEnumerable<Food>, InsertPublicFoodsInUserFoodCommand>
{
    public async Task<IEnumerable<Food>> HandleAsync(InsertPublicFoodsInUserFoodCommand item, CancellationToken cancellationToken)
    {
        var publicFoods = await publicFoodMongoRepository.GetManyByIdsAsync(item.PublicFoodIds, cancellationToken);
        var existingUserFoods = await userFoodMongoRepository.GetAllByUserIdAsync(ProfileContext.ProfileId, cancellationToken);
        var existingIds = existingUserFoods.Select(f => f.Id).ToHashSet();
        
        var newFoods = publicFoods
            .Where(f => !existingIds.Contains(f.Id))
            .Select<Food, Food>(f =>
            {
                var cloned = f.Clone(); // você precisa ter um método Clone() que copia o objeto Food
                cloned.SetId(); // novo ID
                cloned.SetCreatedBy(ProfileContext.ProfileId);
                return cloned;
            })
            .ToList();
        
        if (newFoods.Count != 0)
        {
            await userFoodMongoRepository.InsertManyAsync(newFoods, cancellationToken);
        }

        return newFoods;
    }
}