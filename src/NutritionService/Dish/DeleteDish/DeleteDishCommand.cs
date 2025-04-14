namespace NutritionService.Dish.DeleteDish;

public class DeleteDishCommand(string id)
{
    public string Id { get; set; } = id;
}