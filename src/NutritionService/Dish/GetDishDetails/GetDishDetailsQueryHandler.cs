using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.GetDishDetails;

/// <summary>
/// Handles the retrieval of dish details by ID.
/// </summary>
/// <param name="repository"></param>
public class GetDishDetailsQueryHandler(IDishMongoRepository repository) : IHandler<DishDto, GetDishDetailsQuery>
{
    /// <summary>
    /// Handles the retrieval of dish details based on the provided query.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<DishDto> HandleAsync(GetDishDetailsQuery item, CancellationToken cancellationToken)
    {
        var dish = await repository.GetDishByIdAsync(item.Id);

        if (dish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");

        return new DishDto(dish);
    }
}