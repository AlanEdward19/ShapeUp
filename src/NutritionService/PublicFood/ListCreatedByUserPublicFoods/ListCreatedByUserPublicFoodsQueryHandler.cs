using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;

namespace NutritionService.PublicFood.ListCreatedByUserPublicFoods;

public class ListCreatedByUserPublicFoodsQueryHandler(IPublicFoodMongoRepository repository)
    : IHandler<IEnumerable<FoodDto>, ListCreatedByUserPublicFoodsQuery>
{
    /// <summary>
    /// Handles the retrieval of public food items based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<FoodDto>> HandleAsync(ListCreatedByUserPublicFoodsQuery item, CancellationToken cancellationToken)
    {
        var foods = await repository.ListPublicFoodsCreatedByUserAsync(item.Page, item.Rows);
        var foodsDto = foods.Select(food => new FoodDto(food));
        return foodsDto;
    }
    
}