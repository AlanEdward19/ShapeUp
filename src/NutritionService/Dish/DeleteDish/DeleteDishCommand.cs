namespace NutritionService.Dish.DeleteDish;

public class DeleteDishCommand
{
    public string Id { get; set; }
    
    public DeleteDishCommand(string id)
    {
        Id = id;
    }
}