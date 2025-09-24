namespace NutritionService.UserNutrition.CreateUserNutrition;

public class CreateUserNutritionCommand
{
    public string NutritionManagerId { get; set; }
    public List<string> DailyMenuIds { get; set; } // Alterado para List<string>
    public string UserId { get; private set; } = "";

    public CreateUserNutritionCommand(string nutritionManagerId, List<string> dailyMenuIds)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenuIds = dailyMenuIds;
    }
    
    public void SetUserId(string userId) => UserId = userId;
}