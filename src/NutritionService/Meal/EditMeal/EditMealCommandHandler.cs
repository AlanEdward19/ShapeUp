using NutritionService.Common.Interfaces;
using NutritionService.Dish; // Usar a entidade Ingredient
using NutritionService.Meal.Common;
using SharedKernel.Exceptions;

namespace NutritionService.Meal.EditMeal;

/// <summary>
/// Handles the editing of an existing meal.
/// </summary>
public class EditMealCommandHandler : IHandler<bool, EditMealCommand>
{
    private readonly IMealMongoRepository _mealRepository;

    // Repositórios de Food e Dish não são mais necessários aqui
    public EditMealCommandHandler(IMealMongoRepository mealRepository)
    {
        _mealRepository = mealRepository;
    }

    /// <summary>
    /// Handles the command to edit an existing meal.
    /// </summary>
    public async Task<bool> HandleAsync(EditMealCommand item, CancellationToken cancellationToken)
    {
        var existingMeal = await _mealRepository.GetMealByIdAsync(item.Id);
        
        if (existingMeal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");
        
        // Mapeia os ingredientes do comando para a entidade de domínio
        var newIngredients = item.Ingredients
            .Select(i => new Ingredient(i.FoodId, i.Quantity))
            .ToList();

        // Atualiza a entidade com os novos dados
        existingMeal.UpdateInfo(item.Type, item.Name, item.DishIds, newIngredients);
        
        await _mealRepository.UpdateMealAsync(existingMeal);

        return true;
    }
}