using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.AproveDish;

public class AproveDishCommandHandler(IDishMongoRepository repository) : IHandler<Dish, AproveDishCommand>
{
    public async Task<Dish> HandleAsync(AproveDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await repository.GetDishByBarCodeAsync(item.BarCode);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with barcode {item.BarCode} not found.");

        existingDish.MarkAsRevised();
        
        await repository.UpdateDishAsync(existingDish);

        return existingDish;
    }
}