namespace NutritionService.DailyMenu.EditDailyMenu;

public class EditDailyMenuCommand
{
    public string Id { get; set; } = "";
    public DayOfWeek? DayOfWeek { get; set; }
    public List<Dish.Dish> Dishes { get; set; }
    
    public EditDailyMenuCommand(DayOfWeek? dayOfWeek, List<Dish.Dish> dishes)
    {
        DayOfWeek = dayOfWeek;
        Dishes = dishes;
    }
    
    public void SetId(string id) => Id = id;
}