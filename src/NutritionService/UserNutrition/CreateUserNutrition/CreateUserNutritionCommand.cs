namespace NutritionService.UserNutrition.CreateUserNutrition;

public class CreateUserNutritionCommand
{
    public string NutritionManagerId { get; set; }
    public string[] DailyMenuIds { get; set; }

    
    public CreateUserNutritionCommand(string nutritionManagerId, string[] dailyMenuIds)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenuIds = dailyMenuIds;
    }
}