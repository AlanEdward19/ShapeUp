using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.UserFood.Common.Repository;

namespace NutritionService.UserFood.CreateUserFood;

public class CreateUserFoodCommandHandler(IUserFoodMongoRepository repository) : IHandler<Food, CreateUserFoodCommand>
{
    public async Task<Food> HandleAsync(CreateUserFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetUserFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood != null)
            throw new FoodAlreadyExistsException(command.BarCode);

        var food = command.ToFood();
        
        await repository.CreateUserFoodAsync(food);

        return food;
    }
}