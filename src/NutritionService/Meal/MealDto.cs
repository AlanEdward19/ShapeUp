using NutritionService.Common;
using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal;

public class MealDto(Meal meal)
{
    public string Id { get; set; } = meal.Id;
    public string CreatedBy { get; set; } = meal.CreatedBy;
    public string UserId { get; set; } = meal.UserId;
    public MealType Type { get; set; } = meal.Type;
    public string Name { get; set; } = meal.Name;
    public List<Dish.Dish> Dishes { get; set; } = meal.Dishes.Select(d => d.Clone()).ToList();
    public List<Food> Foods { get; set; } = meal.Foods.Select(d => d.Clone()).ToList();
}