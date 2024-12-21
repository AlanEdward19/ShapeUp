using NutritionService.Food.Common;

namespace NutritionService.Food.CreateFood;

public class CreateFoodCommand(string name, string brand, string barCode, NutritionalInfo nutritionalInfo)
{
    public string Name { get; private set; } = name;
    public string Brand { get; private set; } = brand;
    public string BarCode { get; private set; } = barCode;
    public NutritionalInfo NutritionalInfo { get; private set; } = nutritionalInfo;

    public Food ToFood()
    {
        Food food = new(Name, Brand, BarCode, NutritionalInfo);
        
        food.GenerateId();
        
        return food;
    }
}