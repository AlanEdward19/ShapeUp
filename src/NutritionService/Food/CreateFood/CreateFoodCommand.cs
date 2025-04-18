using NutritionService.Common;
using NutritionService.Food.Common;

namespace NutritionService.Food.CreateFood;

public class CreateFoodCommand
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string BarCode { get; set; }
    public NutritionalInfo NutritionalInfo { get; set; }

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