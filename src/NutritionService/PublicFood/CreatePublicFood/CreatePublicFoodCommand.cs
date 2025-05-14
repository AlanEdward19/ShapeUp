using NutritionService.Common;
using NutritionService.UserFood;
using NutritionService.UserFood.Common;

namespace NutritionService.PublicFood.CreatePublicFood;

public class CreatePublicFoodCommand
{
    public Guid CreatedByGuid { get; set; } = Guid.Empty;
    public string Name { get; set; }
    public string Brand { get; set; }
    public string BarCode { get; set; }
    public NutritionalInfo NutritionalInfo { get; set; }

    public Food ToFood()
    {
        Food food = new(Name, Brand, BarCode, NutritionalInfo);
        
        food.SetId();
        food.SetCreatedBy(ProfileContext.ProfileId);
        
        return food;
    }
    public void SetCreatedBy(Guid createdBy) => CreatedByGuid = createdBy;
    public CreatePublicFoodCommand(string name, string brand, string barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
}