namespace NutritionService.Dish.DeleteDish;

public class DeleteDishCommand
{
    public string Id { get; set; }
    
    public DeleteDishCommand(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id is required.", nameof(id));
        
        Id = id;
    }
}