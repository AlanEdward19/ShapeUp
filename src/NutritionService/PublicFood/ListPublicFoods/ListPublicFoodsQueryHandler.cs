using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.ListPublicFoods;

/// <summary>
/// Handles the listing of public food items.
/// </summary>
/// <param name="repository"></param>
public class ListPublicFoodsQueryHandler(IPublicFoodMongoRepository repository)
: IHandler<IEnumerable<FoodDto>, ListPublicFoodsQuery>
{
    /// <summary>
    /// Handles the retrieval of public food items based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FoodDto>> HandleAsync(ListPublicFoodsQuery item, CancellationToken cancellationToken)
    {
        var foods = await repository.ListPublicFoodsAsync(item.Page, item.Rows);
        var foodsDto = foods.Select(food => new FoodDto(food));
        return foodsDto;
    }
}