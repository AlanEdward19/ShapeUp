namespace NutritionService.PublicFood.ApprovePublicFood;

/// <summary>
/// Command to approve a public food item.
/// </summary>
/// <param name="id"></param>
public class ApprovePublicFoodCommand(string id)
{
    /// <summary>
    /// Identifier of the public food to approve.
    /// </summary>
    public string Id { get; private set; } = id;
}