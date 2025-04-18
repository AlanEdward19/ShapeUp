namespace NutritionService.UserNutrition.CreateUserNutrition;

public class CreateUserNutritionCommand
{
    public string UserId { get; set; }
    public string NutritionManagerId { get; set; }
    public List<DailyMenu.DailyMenu> DailyMenus { get; set; }

    public UserNutrition ToUserNutrition()
    {
        var userNutrition = new UserNutrition(
            userId: UserId,
            nutritionManagerId: NutritionManagerId,
            dailyMenus: DailyMenus
        );
        userNutrition.SetId();
        return userNutrition;
    }
}