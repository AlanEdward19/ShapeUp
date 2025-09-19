namespace NutritionService.UserNutrition;

/// <summary>
/// DTO para representar a nutrição do usuário.
/// </summary>
/// <param name="userNutrition"></param>
public class UserNutritionDto(UserNutrition userNutrition)
{
    /// <summary>
    /// Identificador único da nutrição do usuário.
    /// </summary>
    public string Id { get; set; } = userNutrition.Id;
    /// <summary>
    /// Identificador do usuário que criou a nutrição.
    /// </summary>
    public string CreatedBy { get; set; } = userNutrition.CreatedBy;
    /// <summary>
    /// Identificador do gerente de nutrição associado ao usuário.
    /// </summary>
    public string NutritionManagerId { get; set; } = userNutrition.NutritionManagerId;
    /// <summary>
    /// identificador do usuário que utiliza os serviços de nutrição
    /// </summary>
    public string UserId { get; set; } = userNutrition.UserId;
    /// <summary>
    /// Lista de menus diários do usuário.
    /// </summary>
    public List<DailyMenu.DailyMenu> DailyMenus { get; set; } = userNutrition.DailyMenus
        .Select(d => d.Clone()).ToList();
}