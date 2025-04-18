namespace NutritionService.UserNutrition.EditUserNutrition;

public class EditUserNutritionCommand
{
    public string Id { get; set; } = "";
    public string UserId { get; set; }
    public string NutritionManagerId { get; set; }
    public List<DailyMenu.DailyMenu> DailyMenus { get; set; }
    
    public EditUserNutritionCommand(string userId, string nutritionManagerId, List<DailyMenu.DailyMenu> dailyMenus)
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