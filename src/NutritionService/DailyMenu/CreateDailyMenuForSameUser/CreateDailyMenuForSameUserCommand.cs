namespace NutritionService.DailyMenu.CreateDailyMenuForSameUser;

public class CreateDailyMenuForSameUserCommand
{
    public DayOfWeek? DayOfWeek { get; set; }
    public List<string> MealIds { get; set; } // Alterado para List<string>
    
    public CreateDailyMenuForSameUserCommand(DayOfWeek? dayOfWeek, List<string> mealIds)
    {
        DayOfWeek = dayOfWeek;
        MealIds = mealIds;
    }
}