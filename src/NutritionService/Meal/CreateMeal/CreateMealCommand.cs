using NutritionService.Common;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common.Enums;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Meal.CreateMeal;

public class CreateMealCommand
{
    public MealType Type { get; set; }
    public string Name { get; set; }
    public string[] DishIds { get; set; }
    public string[] FoodIds { get; set; }

    public CreateMealCommand(MealType type, string name, string[] dishIds, string[] foodIds)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        FoodIds = foodIds;
    }
}