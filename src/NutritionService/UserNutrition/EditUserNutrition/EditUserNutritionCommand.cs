namespace NutritionService.UserNutrition.EditUserNutrition;

public class EditUserNutritionCommand
{
    public string? Id { get; set; }
    public string UserId { get; set; }
    public string NutritionManagerId { get; set; }
    public string[] DailyMenus { get; set; }
    
    public EditUserNutritionCommand(string userId, string nutritionManagerId, string[] dailyMenus)
    {
        UserId = userId;
        NutritionManagerId = nutritionManagerId;
        DailyMenus = dailyMenus;
    }
    
    public void SetId(string id)
    {
        Id = id;
    }
}