using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;

namespace NutritionService.PublicFood.CreatePublicFood;

public class CreatePublicFoodCommandHandler(IPublicFoodMongoRepository repository) : IHandler<FoodDto, CreatePublicFoodCommand>
{
    public async Task<FoodDto> HandleAsync(CreatePublicFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetPublicFoodByBarCodeAsync(command.BarCode);
        
        if (existingFood != null)
            throw new FoodAlreadyExistsException(command.BarCode);

        var food = command.ToFood(command.CreatedBy);
        
        await repository.CreatePublicFoodAsync(food);

        return new FoodDto(food);
    }
}