using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.Dish.EditDish;

/// <summary>
/// Handles the command to edit an existing dish.
/// </summary>
public class EditDishCommandHandler : IHandler<bool, EditDishCommand>
{
    private readonly IDishMongoRepository _dishRepository;

    // O foodRepository não é mais necessário aqui.
    public EditDishCommandHandler(IDishMongoRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    /// <summary>
    /// Handles the command to edit an existing dish by updating its name and ingredients.
    /// </summary>
    public async Task<bool> HandleAsync(EditDishCommand item, CancellationToken cancellationToken)
    {
        var existingDish = await _dishRepository.GetDishByIdAsync(item.Id);
        
        if (existingDish == null)
            throw new NotFoundException($"Dish with ID {item.Id} not found.");
        
        // 1. Mapeia a lista de inputs para a lista de entidades Ingredient.
        var newIngredients = item.Ingredients
            .Select(i => new Ingredient(i.FoodId, i.Quantity))
            .ToList();

        // 2. Chama o método UpdateInfo com a nova lista de ingredientes.
        existingDish.UpdateInfo(item.Name, newIngredients);
        
        await _dishRepository.UpdateDishAsync(existingDish);
        
        return true;
    }
}