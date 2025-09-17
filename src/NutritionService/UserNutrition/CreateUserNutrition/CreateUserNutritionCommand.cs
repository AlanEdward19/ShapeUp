namespace NutritionService.UserNutrition.CreateUserNutrition;

public class CreateUserNutritionCommand
{
    public string NutritionManagerId { get; set; }
    public string[] DailyMenuIds { get; set; }
    public string UserId { get; set; } = "";

    
    public CreateUserNutritionCommand(string nutritionManagerId, string[] dailyMenuIds)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenuIds = dailyMenuIds;
    }
    
    public void SetUserId(string userId) => UserId = userId;
}