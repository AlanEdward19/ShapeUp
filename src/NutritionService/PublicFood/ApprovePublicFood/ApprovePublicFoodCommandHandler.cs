using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood.ApproveUserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.ApprovePublicFood;

/// <summary>
/// Handles the approval of public food items.
/// </summary>
/// <param name="repository"></param>
public class ApprovePublicFoodCommandHandler(IPublicFoodMongoRepository repository)
: IHandler<bool, ApprovePublicFoodCommand>
{
    /// <summary>
    /// Handles the approval of a public food item.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(ApprovePublicFoodCommand item, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetPublicFoodByIdAsync(item.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{item.Id}' not found");
        
        existingFood.MarkAsRevised();
        await repository.UpdatePublicFoodAsync(existingFood);
        return true;
    }
}