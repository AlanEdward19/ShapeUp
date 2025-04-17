using NutritionService.Common.Interfaces;
using NutritionService.Food.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Food.ApproveFood;

public class ApproveFoodCommandHandler(IFoodMongoRepository repository) : IHandler<Food, ApproveFoodCommand>
{
    public async Task<Food> HandleAsync(ApproveFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with barcode '{command.Id}' not found");
        
        existingFood.MarkAsRevised();
        
        await repository.UpdateFoodAsync(existingFood);

        return existingFood;
    }
}