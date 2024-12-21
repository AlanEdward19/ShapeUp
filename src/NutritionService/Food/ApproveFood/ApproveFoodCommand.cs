namespace NutritionService.Food.ApproveFood;

public class ApproveFoodCommand(string barCode)
{
    public string BarCode { get; private set; } = barCode;
}