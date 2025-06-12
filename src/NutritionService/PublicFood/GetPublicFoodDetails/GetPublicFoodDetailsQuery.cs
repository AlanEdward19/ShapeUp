namespace NutritionService.PublicFood.GetPublicFoodDetails;

public class GetPublicFoodDetailsQuery(string id)
{
    public string Id { get; private set; } = id;
}