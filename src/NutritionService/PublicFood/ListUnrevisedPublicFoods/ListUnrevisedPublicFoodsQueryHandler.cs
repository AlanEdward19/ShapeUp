using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.ListUnrevisedPublicFoods;

public class ListUnrevisedPublicFoodsQueryHandler(IPublicFoodMongoRepository repository) : IHandler<IEnumerable<Food>, ListUnrevisedPublicFoodsQuery>
{
    public async Task<IEnumerable<Food>> HandleAsync(ListUnrevisedPublicFoodsQuery query, CancellationToken cancellationToken)
    {
        return await repository.ListUnrevisedPublicFoodsAsync(query.Page, query.Rows);
    }
}