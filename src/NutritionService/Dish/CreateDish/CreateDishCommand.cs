
using NutritionService.UserFood;

namespace NutritionService.Dish.CreateDish;

public class CreateDishCommand
{
    public string Name { get; set; }
    public List<Food> Foods { get; set; }
    
    public CreateDishCommand(string name, List<Food> foods)
    {
        Name = name;
        Foods = foods;
    }
    
    public Dish ToDish()
    {
        Dish dish = new(Name, Foods);
        
        dish.SetId();
        
        return dish;
    }
    
    
}