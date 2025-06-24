using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.EditPublicFood;

/// <summary>
/// Command handler for editing public food items.
/// </summary>
/// <param name="repository"></param>
public class EditPublicFoodCommandHandler(IPublicFoodMongoRepository repository) : IHandler<bool, EditPublicFoodCommand>
{
    /// <summary>
    /// Handles the command to edit a public food item.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(EditPublicFoodCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Id))
        {
            throw new ArgumentException("Id is required", nameof(command.Id));
        }
        
        var existingFood = await repository.GetPublicFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{command.Id}' not found");
        
        existingFood.UpdateInfo(command.Name, command.Brand, command.BarCode, command.NutritionalInfo);
        
        await repository.UpdatePublicFoodAsync(existingFood);

        return true;
    }
}