namespace NutritionService.DailyMenu.CreateDailyMenuForDifferentUser;

public class CreateDailyMenuForDifferentUserCommand
{
    public DayOfWeek? DayOfWeek { get; set; }
    public string[] MealIds { get; set; }
    public string UserId { get; set; } = "";
    
    /// <summary>
    /// Creates a command to create a daily menu.
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <param name="mealIds"></param>
    /// <param name="userId"></param>
    public CreateDailyMenuForDifferentUserCommand(DayOfWeek? dayOfWeek, string[] mealIds)
    {
        DayOfWeek = dayOfWeek;
        MealIds = mealIds; 
    }
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}