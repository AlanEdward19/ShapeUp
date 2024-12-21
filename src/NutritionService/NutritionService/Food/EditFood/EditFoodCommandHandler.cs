using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.Food.Common.Repository;

namespace NutritionService.Food.EditFood;

public class EditFoodCommandHandler(IFoodMongoRepository repository) : IHandler<Food, EditFoodCommand>
{
    public async Task<Food> HandleAsync(EditFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with barcode '{command.BarCode}' not found");
        
        Food food = command.ToFood();
        
        await repository.UpdateFoodAsync(food);

        return food;
    }
}