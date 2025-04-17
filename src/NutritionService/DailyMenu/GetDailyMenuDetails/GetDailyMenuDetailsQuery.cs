namespace NutritionService.DailyMenu.GetDailyMenuDetails;

public class GetDailyMenuDetailsQuery(string id)
{
    public string Id { get; set; } = id;
}