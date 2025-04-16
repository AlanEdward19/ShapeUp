using NutritionService.Common;
using NutritionService.Food.Common;

namespace NutritionService.Food.EditFood;

public class EditFoodCommand(string name, string brand, string barCode, NutritionalInfo nutritionalInfo)
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = name;
    public string Brand { get; set; } = brand;
    public string BarCode { get; set; } = barCode;
    public NutritionalInfo NutritionalInfo { get; set; } = nutritionalInfo;

    public void SetId(string id)
    {
        Id = id;
    }
}