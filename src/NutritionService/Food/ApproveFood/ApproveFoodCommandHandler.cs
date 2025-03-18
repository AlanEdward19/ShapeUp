using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.Food.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Food.ApproveFood;

public class ApproveFoodCommandHandler(IFoodMongoRepository repository) : IHandler<Food, ApproveFoodCommand>
{
    public async Task<Food> HandleAsync(ApproveFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with barcode '{command.BarCode}' not found");
        
        existingFood.MarkAsRevised();
        
        await repository.UpdateFoodAsync(existingFood);

        return existingFood;
    }
}