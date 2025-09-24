using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal.CreateMealForSameUser;

// Reutilize a classe IngredientInput
public class IngredientInput
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}

public class CreateMealForSameUserCommand
{
    public MealType Type { get; set; }
    public string Name { get; set; }
    public List<string> DishIds { get; set; }
    public List<IngredientInput> Ingredients { get; set; }

    public CreateMealForSameUserCommand(MealType type, string name, List<string> dishIds, List<IngredientInput> ingredients)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        Ingredients = ingredients;
    }
}