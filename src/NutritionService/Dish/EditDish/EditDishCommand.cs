using NutritionService.Common;
using NutritionService.UserFood;

namespace NutritionService.Dish.EditDish;

public class EditDishCommand
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public string[] FoodIds { get; set; }
    
    public EditDishCommand(string name, string[] foodIds)
    {
        Name = name;
        FoodIds = foodIds;
    }

    public void SetId(string id) => Id = id;
}