namespace NutritionService.UserFood.GetUserFoodDetails;

public class GetUserFoodDetailsQuery(string id)
{
    public string Id { get; private set; } = id;
}