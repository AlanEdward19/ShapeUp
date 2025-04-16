using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.EditDish;

public class EditDishCommandHandler(IDishMongoRepository repository) : IHandler<Dish, EditDishCommand>
{
    public async Task<Dish> HandleAsync(EditDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await repository.GetDishByIdAsync(item.Id);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");

        existingDish.UpdateInfo(item.Name, item.Foods);
        
        await repository.UpdateDishAsync(existingDish);

        return existingDish;
    }
}
