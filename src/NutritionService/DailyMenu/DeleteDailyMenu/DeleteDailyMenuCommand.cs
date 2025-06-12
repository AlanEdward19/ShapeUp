namespace NutritionService.DailyMenu.DeleteDailyMenu;

public class DeleteDailyMenuCommand
{
    public string Id { get; set; }
    
    public DeleteDailyMenuCommand(string id)
    {
        Id = id;
    }
}