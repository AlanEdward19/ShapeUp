namespace NutritionService.UserNutrition.EditUserNutrition;

public class EditUserNutritionCommand
{
    public string? Id { get; set; }
    public string NutritionManagerId { get; set; }
    public List<string> DailyMenuIds { get; set; } 
    
    public EditUserNutritionCommand(string nutritionManagerId, List<string> dailyMenuIds)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenuIds = dailyMenuIds;
    }
    
    public void SetId(string id)
    {
        Id = id;
    }
}