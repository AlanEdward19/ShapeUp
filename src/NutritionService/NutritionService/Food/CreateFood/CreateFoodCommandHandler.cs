using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.Food.Common.Repository;

namespace NutritionService.Food.CreateFood;

public class CreateFoodCommandHandler(IFoodMongoRepository repository) : IHandler<Food, CreateFoodCommand>
{
    public async Task<Food> HandleAsync(CreateFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood != null)
            throw new FoodAlreadyExistsException(command.BarCode);

        Food food = command.ToFood();
        
        await repository.InsertFoodAsync(food);

        return food;
    }
}