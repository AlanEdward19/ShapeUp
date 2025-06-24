using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.ListRevisedPublicFoods;

/// <summary>
/// Handles the listing of revised public food items.
/// </summary>
/// <param name="repository"></param>
public class ListRevisedPublicFoodsQueryHandler(IPublicFoodMongoRepository repository)
: IHandler<IEnumerable<FoodDto>, ListRevisedPublicFoodsQuery>
{
    /// <summary>
    /// Handles the retrieval of revised public food items based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FoodDto>> HandleAsync(ListRevisedPublicFoodsQuery item, CancellationToken cancellationToken)
    {
        var foods = await repository.ListRevisedPublicFoodsAsync(item.Page, item.Rows);
        var foodsDto = foods.Select(food => new FoodDto(food));
        return foodsDto;
    }
}