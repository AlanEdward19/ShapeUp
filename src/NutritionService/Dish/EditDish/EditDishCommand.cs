using NutritionService.Common;
using NutritionService.UserFood;

namespace NutritionService.Dish.EditDish;

public class EditDishCommand
{
    public string? Id { get; set; }
    public string Name { get; set; }
    
    // Alterado de string[] para List<IngredientInput>
    public List<IngredientInput> Ingredients { get; set; } 

    public EditDishCommand(string name, List<IngredientInput> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }

    public void SetId(string id) => Id = id;
}
/// <summary>
/// Represents a single ingredient for the EditDishCommand.
/// </summary>
public class IngredientInput
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}