using NutritionService.Food.Common;

namespace NutritionService.Food.EditFood;

public class EditFoodCommand(string name, string brand, NutritionalInfo nutritionalInfo)
{
    public string Name { get; private set; } = name;
    public string Brand { get; private set; } = brand;
    public string BarCode { get; private set; } = "";
    public NutritionalInfo NutritionalInfo { get; private set; } = nutritionalInfo;

    public void SetBarCode(string barCode)
    {
        BarCode = barCode;
    }
    
    public Food ToFood()
    {
        return new Food(Name, Brand, BarCode, NutritionalInfo);
    }
}