using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.DeleteDish;

public class DeleteDishCommandHandler(IDishMongoRepository repository) : IHandler<Dish, DeleteDishCommand>
{
    public async Task<Dish> HandleAsync(DeleteDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await repository.GetDishByIdAsync(item.Id);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");

        await repository.DeleteDishAsync(item.Id);
        
        return existingDish;
    }
}