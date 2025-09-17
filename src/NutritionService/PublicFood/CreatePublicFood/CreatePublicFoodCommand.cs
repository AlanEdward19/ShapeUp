using NutritionService.Common;
using NutritionService.UserFood;
using NutritionService.UserFood.Common;

namespace NutritionService.PublicFood.CreatePublicFood;

public class CreatePublicFoodCommand
{
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? BarCode { get; set; }
    public NutritionalInfo NutritionalInfo { get; set; }
    public string UserId { get; set; } = "";

    public Food ToFood()
    {
        
        Food food = new(Name, Brand, BarCode, NutritionalInfo);
        
        return food;
    }
    public CreatePublicFoodCommand(string name, string? brand, string? barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
    
    public void SetUserId(string id)
    {
        UserId = id;
    }
}