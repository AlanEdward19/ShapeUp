using NutritionService.Common;
using NutritionService.Meal.Common.Enums;
using NutritionService.UserFood;

namespace NutritionService.Meal.EditMeal;

public class EditMealCommand
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public MealType Type { get; set; }
    public string[] DishIds { get; set; }
    public string[] FoodIds { get; set; }
    
    public EditMealCommand(string name, MealType type, string[] dishIds, string[] foodIds)
    {
        Name = name;
        Type = type;
        DishIds = dishIds;
        FoodIds = foodIds;
    }

    public void SetId(string id) => Id = id;
}