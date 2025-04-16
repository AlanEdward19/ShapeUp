namespace NutritionService.Dish.EditDish;

public class EditDishCommand
{
    public string Id { get; set; } = "";
    public string Name { get; set; }
    public List<Food.Food> Foods { get; set; }
    
    public EditDishCommand(string name, List<Food.Food> foods)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        
        if (foods == null || foods.Count == 0)
            throw new ArgumentException("Foods are required.", nameof(foods));

        Name = name;
        Foods = foods;
    }

    public void SetId(string id) => Id = id;
}