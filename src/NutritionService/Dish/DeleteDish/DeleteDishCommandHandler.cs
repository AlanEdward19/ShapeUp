using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.DeleteDish;

/// <summary>
/// Handles the deletion of a dish.
/// </summary>
/// <param name="repository"></param>
public class DeleteDishCommandHandler(IDishMongoRepository repository) : IHandler<bool, DeleteDishCommand>
{
    /// <summary>
    /// Handles the deletion of a dish by its ID.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(DeleteDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await repository.GetDishByIdAsync(item.Id);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");

        await repository.DeleteDishAsync(item.Id);
        
        return true;
    }
}