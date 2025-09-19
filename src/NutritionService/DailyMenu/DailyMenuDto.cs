namespace NutritionService.DailyMenu;

/// <summary>
/// DTO para representar um menu diário.
/// </summary>
/// <param name="dailyMenu"></param>
public class DailyMenuDto(DailyMenu dailyMenu)
{
    /// <summary>
    /// Identificador único do menu diário.
    /// </summary>
    public string Id { get; set; } = dailyMenu.Id;
    /// <summary>
    /// Identificador do usuário que criou o menu diário.
    /// </summary>
    public string CreatedBy { get; set; } = dailyMenu.CreatedBy;
    public string UserId { get; set; } = dailyMenu.UserId;
    /// <summary>
    /// Dia da semana do menu diário.
    /// </summary>
    public DayOfWeek? DayOfWeek { get; set; } = dailyMenu.DayOfWeek;
    /// <summary>
    /// Lista de refeições do menu diário.
    /// </summary>
    public List<Meal.Meal> Meals { get; set; } = dailyMenu.Meals.Select(m => m.Clone()).ToList();
}