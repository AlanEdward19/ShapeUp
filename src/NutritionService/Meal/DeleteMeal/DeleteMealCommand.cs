namespace NutritionService.Meal.DeleteMeal;

public class DeleteMealCommand
{
    public string Id { get; set; }

    public DeleteMealCommand(string id)
    {
        Id = id;
    }
}