using NutritionService.Common.Interfaces;
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.EditMeal;

public class EditMealCommandHandler(IMealMongoRepository repository) : IHandler<Meal, EditMealCommand>
{
    public async Task<Meal> HandleAsync(EditMealCommand item, CancellationToken cancellationToken)
    {
        var existingMeal = await repository.GetMealByIdAsync(item.Id);
        
        if (existingMeal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");

        existingMeal.UpdateInfo(item.Type, item.Name, item.Dishes, item.Foods);
        
        await repository.UpdateMealAsync(existingMeal);

        return existingMeal;
    }
}