using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.GetDishDetails;

public class GetDishDetailsQueryHandler(IDishMongoRepository repository) : IHandler<Dish, GetDishDetailsQuery>
{
    public async Task<Dish> HandleAsync(GetDishDetailsQuery item, CancellationToken cancellationToken)
    {
        var dish = await repository.GetDishByIdAsync(item.Id);

        if (dish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");

        return dish;
    }
}