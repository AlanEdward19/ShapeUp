using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.ListRevisedPublicFoods;

public class ListRevisedPublicFoodsQueryHandler(IPublicFoodMongoRepository repository)
: IHandler<IEnumerable<Food>, ListRevisedPublicFoodsQuery>
{
    public async Task<IEnumerable<Food>> HandleAsync(ListRevisedPublicFoodsQuery item, CancellationToken cancellationToken)
    {
        return await repository.ListRevisedPublicFoodsAsync(item.Page, item.Rows);
    }
}