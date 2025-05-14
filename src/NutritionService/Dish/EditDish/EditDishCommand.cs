using NutritionService.UserFood;

namespace NutritionService.Dish.EditDish;

public class EditDishCommand
{
    public string Id { get; set; } = "";
    public string Name { get; set; }
    public List<Food> Foods { get; set; }
    
    public EditDishCommand(string name, List<Food> foods)
    {
        Name = name;
        Foods = foods;
    }

    public void SetId(string id) => Id = id;
}