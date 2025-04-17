namespace NutritionService.DailyMenu.ListDailyMenus;

public class ListDailyMenuQuery
{
    public string? DayOfWeek { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}