using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.ListPublicFoods;

public class ListPublicFoodsQueryHandler(IPublicFoodMongoRepository repository)
: IHandler<IEnumerable<Food>, ListPublicFoodsQuery>
{
    public async Task<IEnumerable<Food>> HandleAsync(ListPublicFoodsQuery item, CancellationToken cancellationToken)
    {
        return await repository.ListPublicFoodsAsync(item.Page, item.Rows);
    }
}