namespace NutritionService.UserFood.ApproveUserFood;

public class ApproveUserFoodCommand(string id)
{
    public string Id { get; private set; } = id;
}