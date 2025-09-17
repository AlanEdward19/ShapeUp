using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;

namespace NutritionService.Dish.ListDishes;

/// <summary>
/// ListDishesQueryHandler handles the query to list dishes.
/// </summary>
/// <param name="repository"></param>
public class ListDishesQueryHandler(IDishMongoRepository repository)
: IHandler<IEnumerable<DishDto>, ListDishesQuery>
{
    /// <summary>
    /// Handles the retrieval of dishes based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<DishDto>> HandleAsync(ListDishesQuery item, CancellationToken cancellationToken)
    {
        var dishes = await repository.ListDihesAsync(item.Page, item.Rows, cancellationToken, item.UserId);
        var dishesDto = dishes.Select(dish => new DishDto(dish));
        return dishesDto;
    }
}