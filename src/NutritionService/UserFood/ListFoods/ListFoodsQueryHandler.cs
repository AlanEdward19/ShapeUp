using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;

namespace NutritionService.UserFood.ListFoods;

public class ListFoodsQueryHandler(IUserFoodMongoRepository repository) : IHandler<IEnumerable<FoodDto>, ListFoodsQuery>
{
    public async Task<IEnumerable<FoodDto>> HandleAsync(ListFoodsQuery query, CancellationToken cancellationToken)
    {
        var foods = await repository.ListFoodsAsync(query.Page, query.Rows);
        
        var foodsDto = foods.Select(food => new FoodDto(food));
        return foodsDto;
    }
}