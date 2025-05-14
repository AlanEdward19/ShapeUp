using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.EditPublicFood;

public class EditPublicFoodCommandHandler(IPublicFoodMongoRepository repository) : IHandler<Food, EditPublicFoodCommand>
{
    public async Task<Food> HandleAsync(EditPublicFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetPublicFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{command.Id}' not found");
        
        existingFood.UpdateInfo(command.Name, command.Brand, command.BarCode, command.NutritionalInfo);
        
        await repository.UpdatePublicFoodAsync(existingFood);

        return existingFood;
    }
}