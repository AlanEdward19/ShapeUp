using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.EditUserFood;

public class EditUserFoodCommandHandler(IUserFoodMongoRepository repository) : IHandler<bool, EditUserFoodCommand>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(EditUserFoodCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Id))
        {
            throw new ArgumentException("Id is required", nameof(command.Id));
        }
        
        var existingFood = await repository.GetUserFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{command.Id}' not found");
        
        existingFood.UpdateInfo(command.Name, command.Brand, command.BarCode, command.NutritionalInfo);
        
        await repository.UpdateUserFoodAsync(existingFood);

        return true;
    }
}