using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;

namespace NutritionService.UserFood.ListFoods;

public class ListFoodsQueryHandler(IUserFoodMongoRepository repository) : IHandler<IEnumerable<Food>, ListFoodsQuery>
{
    public async Task<IEnumerable<Food>> HandleAsync(ListFoodsQuery query, CancellationToken cancellationToken)
    {
        return await repository.ListFoodsAsync(query.Page, query.Rows);
    }
}