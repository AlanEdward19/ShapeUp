using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal.CreateMealForSameUser;

public class CreateMealForSameUserCommand
{
    public MealType Type { get; set; }
    public string Name { get; set; }
    public string[] DishIds { get; set; }
    public string[] FoodIds { get; set; }

    public CreateMealForSameUserCommand(MealType type, string name, string[] dishIds, string[] foodIds)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        FoodIds = foodIds;
    }
}