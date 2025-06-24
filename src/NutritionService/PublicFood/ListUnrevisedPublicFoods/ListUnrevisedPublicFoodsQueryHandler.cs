using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.ListUnrevisedPublicFoods;

/// <summary>
/// Handles the listing of unrevised public food items.
/// </summary>
/// <param name="repository"></param>
public class ListUnrevisedPublicFoodsQueryHandler(IPublicFoodMongoRepository repository) : IHandler<IEnumerable<FoodDto>, ListUnrevisedPublicFoodsQuery>
{
    /// <summary>
    /// Handles the retrieval of unrevised public food items based on pagination parameters.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FoodDto>> HandleAsync(ListUnrevisedPublicFoodsQuery query, CancellationToken cancellationToken)
    {
        var foods = await repository.ListUnrevisedPublicFoodsAsync(query.Page, query.Rows);
        var foodsDto = foods.Select(food => new FoodDto(food));
        return foodsDto;
    }
}