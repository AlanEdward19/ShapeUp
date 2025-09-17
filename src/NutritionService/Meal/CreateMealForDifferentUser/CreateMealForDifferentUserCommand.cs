using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal.CreateMealForDifferentUser;

public class CreateMealForDifferentUserCommand
{
    public MealType Type { get; set; }
    public string Name { get; set; }
    public string[] DishIds { get; set; }
    public string[] FoodIds { get; set; }
    public string UserId { get; set; } = "";

    public CreateMealForDifferentUserCommand(MealType type, string name, string[] dishIds, string[] foodIds)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        FoodIds = foodIds;
    }
    
    public void SetUserId(string userId) => UserId = userId;
}