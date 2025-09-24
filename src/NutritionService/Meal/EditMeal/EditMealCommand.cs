using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal.EditMeal;

// Reutilize a classe IngredientInput que já definimos anteriormente
public class IngredientInput
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}

public class EditMealCommand
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public MealType Type { get; set; }
    public List<string> DishIds { get; set; }
    public List<IngredientInput> Ingredients { get; set; }
    
    public EditMealCommand(string name, MealType type, List<string> dishIds, List<IngredientInput> ingredients)
    {
        Name = name;
        Type = type;
        DishIds = dishIds;
        Ingredients = ingredients;
    }

    public void SetId(string id) => Id = id;
}