using SharedKernel.Utils;

namespace NutritionService.DailyMenu.CreateDailyMenu;

public class CreateDailyMenuCommand
{
    public DayOfWeek? DayOfWeek { get; set; }
    public string[] MealIds { get; set; }
    
    /// <summary>
    /// Creates a command to create a daily menu.
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <param name="mealIds"></param>
    public CreateDailyMenuCommand(DayOfWeek? dayOfWeek, string[] mealIds)
    {
        DayOfWeek = dayOfWeek;
        MealIds = mealIds;
    }
}