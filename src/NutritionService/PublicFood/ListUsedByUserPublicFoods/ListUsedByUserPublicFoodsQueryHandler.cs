using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;

namespace NutritionService.PublicFood.ListUsedByUserPublicFoods;

public class ListUsedByUserPublicFoodsQueryHandler(IPublicFoodMongoRepository repository)
    : IHandler<IEnumerable<FoodDto>, ListUsedByUserPublicFoodsQuery>
{
    /// <summary>
    /// Handles the retrieval of public food items based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FoodDto>> HandleAsync(ListUsedByUserPublicFoodsQuery item, CancellationToken cancellationToken)
    {
        var foods = await repository.ListPublicFoodsUsedByUserAsync(item.Page, item.Rows, item.UserId);
        var foodsDto = foods.Select(food => new FoodDto(food));
        return foodsDto;
    }
}