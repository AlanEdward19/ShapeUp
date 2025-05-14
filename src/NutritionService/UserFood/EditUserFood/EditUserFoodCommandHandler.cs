using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.EditUserFood;

public class EditUserFoodCommandHandler(IUserFoodMongoRepository repository) : IHandler<Food, EditUserFoodCommand>
{
    public async Task<Food> HandleAsync(EditUserFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetUserFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{command.Id}' not found");
        
        existingFood.UpdateInfo(command.Name, command.Brand, command.BarCode, command.NutritionalInfo);
        
        await repository.UpdateUserFoodAsync(existingFood);

        return existingFood;
    }
}