using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;
using SharedKernel.Utils;

namespace NutritionService.Dish.EditDish;

public class EditDishCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository foodRepository) : IHandler<Dish, EditDishCommand>
{
    public async Task<Dish> HandleAsync(EditDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await dishRepository.GetDishByIdAsync(item.Id);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");
        
        var foodItems = await foodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);

        existingDish.UpdateInfo(item.Name, foodItems.ToList());
        
        await dishRepository.UpdateDishAsync(existingDish);

        

        return existingDish;
    }
}
