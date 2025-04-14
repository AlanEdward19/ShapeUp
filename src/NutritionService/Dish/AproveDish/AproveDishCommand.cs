namespace NutritionService.Dish.AproveDish;

public class AproveDishCommand(string barCode)
{
    public string BarCode { get; private set; } = barCode;
}