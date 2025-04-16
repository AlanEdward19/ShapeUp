using NutritionService.Common.Interfaces;
using NutritionService.Food.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Food.EditFood;

public class EditFoodCommandHandler(IFoodMongoRepository repository) : IHandler<Food, EditFoodCommand>
{
    public async Task<Food> HandleAsync(EditFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{command.Id}' not found");
        
        existingFood.UpdateInfo(command.Name, command.Brand, command.BarCode, command.NutritionalInfo);
        
        await repository.UpdateFoodAsync(existingFood);

        return existingFood;
    }
}