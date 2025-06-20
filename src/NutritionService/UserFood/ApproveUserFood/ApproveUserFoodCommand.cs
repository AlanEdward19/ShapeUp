namespace NutritionService.UserFood.ApproveUserFood;

/// <summary>
/// Command to approve user food.
/// </summary>
/// <param name="id"></param>
public class ApproveUserFoodCommand(string id)
{
    /// <summary>
    /// Identifier of the user food to approve.
    /// </summary>
    public string Id { get; private set; } = id;
}