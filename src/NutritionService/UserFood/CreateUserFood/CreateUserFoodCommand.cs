using NutritionService.Common;
using NutritionService.UserFood.Common;

namespace NutritionService.UserFood.CreateUserFood;

public class CreateUserFoodCommand
{
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? BarCode { get; set; }
    public NutritionalInfo NutritionalInfo { get; set; }

    public Food ToFood()
    {
        Food food = new(Name, Brand, BarCode, NutritionalInfo);
        
        return food;
    }
    public CreateUserFoodCommand(string name, string? brand, string? barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
}