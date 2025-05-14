using NutritionService.Meal.Common.Enums;
using NutritionService.UserFood;

namespace NutritionService.Meal.EditMeal;

public class EditMealCommand
{
    public string Id { get; set; } = "";
    public string Name { get; set; }
    public MealType Type { get; set; }
    public List<Dish.Dish> Dishes { get; set; }
    public List<Food> Foods { get; set; }
    
    public EditMealCommand(string id, string name, MealType type, List<Dish.Dish> dishes, List<Food> foods)
    {
        Name = name;
        Type = type;
        Dishes = dishes;
        Foods = foods;
    }

    public void SetId(string id) => Id = id;
}