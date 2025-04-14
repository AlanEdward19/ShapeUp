using NutritionService.Common;

namespace NutritionService.Dish.CreateDish;

public class CreateDishCommand
{
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? BarCode { get; set; }
    public NutritionalInfo? NutritionalInfo { get; set; }
    public List<Food.Food> Foods { get; set; }
    
    public CreateDishCommand(string name, List<Food.Food> foods)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        
        if (foods == null || foods.Count == 0)
            throw new ArgumentException("Foods are required.", nameof(foods));

        Name = name;
        Foods = foods;
    }
    
    public Dish ToDish()
    {
        Dish dish = new(Name, Brand, BarCode, NutritionalInfo, Foods);
        
        dish.GenerateId();
        
        return dish;
    }
    
    
}