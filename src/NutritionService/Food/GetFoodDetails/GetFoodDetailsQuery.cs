namespace NutritionService.Food.GetFoodDetails;

public class GetFoodDetailsQuery(string id)
{
    public string Id { get; private set; } = id;
}