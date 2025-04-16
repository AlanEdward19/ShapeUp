using NutritionService.Common;
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
        Food food = new(Name, Brand, BarCode, NutritionalInfo);
        
        food.SetId();
        
        return food;
    }
    public CreateFoodCommand(string name, string brand, string barCode, NutritionalInfo nutritionalInfo)
    {
        if (string.IsNullOrWhiteSpace(barCode))
            throw new ArgumentException("Barcode is required.", nameof(barCode));

        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
}