using NutritionService.Common;

namespace NutritionService.Dish;

/// <summary>
/// DTO para representar um prato.
/// </summary>
/// <param name="dish"></param>
public class DishDto(Dish dish)
{
    /// <summary>
    /// Identificador único do prato.
    /// </summary>
    public string Id { get; set; } = dish.Id;
    /// <summary>
    /// Identificador do usuário que criou o prato.
    /// </summary>
    public string CreatedBy { get; set; } = dish.CreatedBy;
    /// <summary>
    /// Nome do prato.
    /// </summary>
    public string Name { get; set; } = dish.Name;
    /// <summary>
    /// Descrição do prato.
    /// </summary>
    public List<Food> Foods { get; set; } = dish.Foods.Select(f => f.Clone()).ToList();
}