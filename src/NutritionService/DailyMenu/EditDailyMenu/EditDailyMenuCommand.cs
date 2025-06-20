namespace NutritionService.DailyMenu.EditDailyMenu;

public class EditDailyMenuCommand
{
    public string? Id { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public string[] MealIds { get; set; }
    
    public EditDailyMenuCommand(DayOfWeek? dayOfWeek, string[] mealIds)
    {
        DayOfWeek = dayOfWeek;
        MealIds = mealIds;
    }
    public EditDailyMenuCommand() { }
    public void SetId(string id) => Id = id;
}