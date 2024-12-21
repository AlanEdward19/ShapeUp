using NutritionService.Common.Interfaces;
using NutritionService.Food.Common.Repository;

namespace NutritionService.Food.ListNonRevisedFoods;

public class ListUnrevisedFoodsQueryHandler(IFoodMongoRepository repository) : IHandler<IEnumerable<Food>, ListUnrevisedFoodsQuery>
{
    public async Task<IEnumerable<Food>> HandleAsync(ListUnrevisedFoodsQuery query, CancellationToken cancellationToken)
    {
        return await repository.ListUnrevisedFoodsAsync();
    }
}