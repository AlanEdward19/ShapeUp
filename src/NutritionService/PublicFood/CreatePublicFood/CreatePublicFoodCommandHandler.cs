using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.CreatePublicFood;

public class CreatePublicFoodCommandHandler(IPublicFoodMongoRepository repository) : IHandler<Food, CreatePublicFoodCommand>
{
    public async Task<Food> HandleAsync(CreatePublicFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetPublicFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood != null)
            throw new FoodAlreadyExistsException(command.BarCode);

        var food = command.ToFood();
        
        await repository.CreatePublicFoodAsync(food);

        return food;
    }
}