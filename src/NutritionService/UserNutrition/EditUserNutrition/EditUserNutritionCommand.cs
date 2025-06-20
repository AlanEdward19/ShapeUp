namespace NutritionService.UserNutrition.EditUserNutrition;

public class EditUserNutritionCommand
{
    public string? Id { get; set; }
    public string NutritionManagerId { get; set; }
    public string[] DailyMenus { get; set; }
    
    public EditUserNutritionCommand(string nutritionManagerId, string[] dailyMenus)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenus = dailyMenus;
    }
    
    public void SetId(string id)
    {
        Id = id;
    }
}