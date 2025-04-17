namespace NutritionService.Food.ApproveFood;

public class ApproveFoodCommand(string id)
{
    public string Id { get; private set; } = id;
}