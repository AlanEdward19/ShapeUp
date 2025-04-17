
namespace NutritionService.Dish.CreateDish;

public class CreateDishCommand
{
    public string Name { get; set; }
    public List<Food.Food> Foods { get; set; }
    
    public CreateDishCommand(string name, List<Food.Food> foods)
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