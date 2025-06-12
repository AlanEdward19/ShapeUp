using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Dish.CreateDish;

public class CreateDishCommand
{
    public string Name { get; set; }
    public string[] FoodIds { get; set; }
    
    public CreateDishCommand(string name, string[] foodIds)
    {
        Name = name;
        FoodIds  = foodIds;
    }
    
    
}