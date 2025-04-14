using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Exceptions;

namespace NutritionService.Dish.CreateDish;

public class CreateDishCommandHandler(IDishMongoRepository repository) : IHandler<Dish, CreateDishCommand>
{
    public async Task<Dish> HandleAsync(CreateDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await repository.GetDishByBarCodeAsync(item.BarCode);
        
        if (existingDish != null)
            throw new DishAlreadyExistsException(item.BarCode);

        var dish = item.ToDish();
        
        await repository.InsertDishAsync(dish);

        return dish;
    }
}