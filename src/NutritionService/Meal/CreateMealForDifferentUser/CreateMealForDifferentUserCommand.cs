using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal.CreateMealForDifferentUser;

// Reutilize a classe IngredientInput
public class IngredientInput
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}

public class CreateMealForDifferentUserCommand
{
    public MealType Type { get; set; }
    public string Name { get; set; }
    public List<string> DishIds { get; set; }
    public List<IngredientInput> Ingredients { get; set; }
    public string UserId { get; private set; } = "";

    public CreateMealForDifferentUserCommand(MealType type, string name, List<string> dishIds, List<IngredientInput> ingredients)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        Ingredients = ingredients;
    }
    
    public void SetUserId(string userId) => UserId = userId;
}