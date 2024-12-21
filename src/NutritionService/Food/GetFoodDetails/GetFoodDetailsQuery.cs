namespace NutritionService.Food.GetFoodDetails;

public class GetFoodDetailsQuery(string barCode)
{
    public string BarCode { get; private set; } = barCode;
}