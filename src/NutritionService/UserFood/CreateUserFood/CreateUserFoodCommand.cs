using NutritionService.Common;
using NutritionService.UserFood.Common;

namespace NutritionService.UserFood.CreateUserFood;

public class CreateUserFoodCommand
{
    public string? CreatedBy { get; set; }
    public string Name { get; set; }
    public string? Brand { get; set; }
    public string? BarCode { get; set; }
    public NutritionalInfo NutritionalInfo { get; set; }

    public Food ToFood(string? createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("CreatedBy is required", nameof(createdBy));
        
        Food food = new(Name, Brand, BarCode, NutritionalInfo);
        
        food.SetId();
        food.SetCreatedBy(CreatedBy!);
        
        return food;
    }
    public void SetCreatedBy(string createdBy) => CreatedBy = createdBy;
    public CreateUserFoodCommand(string name, string? brand, string? barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
}