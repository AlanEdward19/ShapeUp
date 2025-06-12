using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;

namespace NutritionService.Dish.ListDishes;

public class ListDishesQueryHandler(IDishMongoRepository repository)
: IHandler<IEnumerable<Dish>, ListDishesQuery>
{
    public async Task<IEnumerable<Dish>> HandleAsync(ListDishesQuery item, CancellationToken cancellationToken)
    {
        return await repository.ListDihesAsync(item.Page, item.Rows, cancellationToken);
    }
}