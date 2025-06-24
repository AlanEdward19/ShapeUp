using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.UserFood.Common.Repository;

namespace NutritionService.UserFood.CreateUserFood;

public class CreateUserFoodCommandHandler(IUserFoodMongoRepository repository) : IHandler<FoodDto, CreateUserFoodCommand>
{
    public async Task<FoodDto> HandleAsync(CreateUserFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetUserFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood != null)
            throw new FoodAlreadyExistsException(command.BarCode);

        var food = command.ToFood(command.CreatedBy!);
        
        await repository.InsertUserFoodAsync(food);

        return new FoodDto(food);
    }
}