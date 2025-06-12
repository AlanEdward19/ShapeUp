namespace NutritionService.DailyMenu.EditDailyMenu;

public class EditDailyMenuCommand
{
    public string? Id { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public List<Meal.Meal> Meals { get; set; }
    
    public EditDailyMenuCommand(DayOfWeek? dayOfWeek, List<Meal.Meal> meals)
    {
        DayOfWeek = dayOfWeek;
        Meals = meals;
    }
    public EditDailyMenuCommand() { }
    public void SetId(string id) => Id = id;
}