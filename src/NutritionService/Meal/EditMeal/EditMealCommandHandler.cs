using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.EditMeal;

public class EditMealCommandHandler(IMealMongoRepository repository, IUserFoodMongoRepository userFoodRepository, IDishMongoRepository dishRepository) : IHandler<Meal, EditMealCommand>
{
    public async Task<Meal> HandleAsync(EditMealCommand item, CancellationToken cancellationToken)
    {
        var existingMeal = await repository.GetMealByIdAsync(item.Id);
        
        if (existingMeal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");
        
        var builtDishes = await dishRepository.GetManyByIdsAsync(item.DishIds, cancellationToken);
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);

        existingMeal.UpdateInfo(item.Type, item.Name, builtDishes.ToList(), builtFoods.ToList());
        
        await repository.UpdateMealAsync(existingMeal);

        return existingMeal;
    }
}