using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.DeletePublicFood;

/// <summary>
/// Handles the deletion of public food items.
/// </summary>
/// <param name="repository"></param>
public class DeletePublicFoodCommandHandler(IPublicFoodMongoRepository repository)
 : IHandler<bool, DeletePublicFoodCommand>
{ 
    /// <summary>
    /// Handles the deletion of a public food item by its ID.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(DeletePublicFoodCommand item, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetPublicFoodByIdAsync(item.Id);
        
        if (existingFood == null) 
            throw new NotFoundException($"Food with id '{item.Id}' not found");
        
        await repository.DeletePublicFoodAsync(item.Id);
        
        return true;
    }
}