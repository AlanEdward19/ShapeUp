using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;

namespace NutritionService.UserFood.ListUnrevisedFoods;

public class ListUnrevisedFoodsQueryHandler(IUserFoodMongoRepository repository) : IHandler<IEnumerable<Food>, ListUnrevisedFoodsQuery>
{
    public async Task<IEnumerable<Food>> HandleAsync(ListUnrevisedFoodsQuery query, CancellationToken cancellationToken)
    {
        return await repository.ListUnrevisedFoodsAsync();
    }
}