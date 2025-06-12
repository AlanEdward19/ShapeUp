using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.DeletePublicFood;

public class DeletePublicFoodCommandHandler(IPublicFoodMongoRepository repository)
 : IHandler<Food, DeletePublicFoodCommand>
{ 
    public async Task<Food> HandleAsync(DeletePublicFoodCommand item, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetPublicFoodByIdAsync(item.Id);
        
        if (existingFood == null) 
            throw new NotFoundException($"Food with id '{item.Id}' not found");
        
        await repository.DeletePublicFoodAsync(item.Id);
        
        return existingFood;
    }
}