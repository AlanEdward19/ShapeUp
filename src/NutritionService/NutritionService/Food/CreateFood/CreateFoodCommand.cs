using NutritionService.Food.Common;

namespace NutritionService.Food.CreateFood;

public class CreateFoodCommand
{
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public string BarCode { get; private set; }
    public NutritionalInfo NutritionalInfo { get; private set; }
    
    public Food ToFood()
    {
        return new Food(Name, Brand, BarCode, NutritionalInfo);
    }
}