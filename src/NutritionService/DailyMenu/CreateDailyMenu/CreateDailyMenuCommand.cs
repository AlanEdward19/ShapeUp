namespace NutritionService.DailyMenu.CreateDailyMenu;

public class CreateDailyMenuCommand
{
    public DayOfWeek? DayOfWeek { get; set; }
    public List<Meal.Meal> Meals { get; set; }
    
    public CreateDailyMenuCommand(DayOfWeek? dayOfWeek, List<Meal.Meal> meals)
    {
        DayOfWeek = dayOfWeek;
        Meals = meals;
    }
    
    public DailyMenu ToDailyMenu()
    {
        DailyMenu dailyMenu = new(DayOfWeek, Meals);
        
        dailyMenu.SetId();
        
        return dailyMenu;
    }
}