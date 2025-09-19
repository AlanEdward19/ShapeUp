namespace NutritionService.Common;

/// <summary>
/// Classe que representa um DTO (Data Transfer Object) para o alimento
/// </summary>
/// <param name="food"></param>
public class FoodDto(Food food)
{
    /// <summary>
    /// Identificador do alimento
    /// </summary>
    public string Id { get; set; } = food.Id;
    /// <summary>
    /// Identificador do perfil que criou o alimento
    /// </summary>
    public string CreatedBy { get; set; } = food.CreatedBy;
    /// <summary>
    /// identificador do usuário que utiliza os serviços de nutrição
    /// </summary>
    public string UserId { get; set; } = food.UserId;
    /// <summary>
    /// Nome do alimento
    /// </summary>
    public string Name { get; set; } = food.Name;
    /// <summary>
    /// Marca do alimento
    /// </summary>
    public string? Brand { get; set; } = food.Brand;
    /// <summary>
    /// Código de barras do alimento
    /// </summary>
    public string? BarCode { get; set; } = food.BarCode;
    /// <summary>
    /// Classificação de revisão do alimento
    /// </summary>
    public bool IsRevised { get; set; } = food.IsRevised;
    /// <summary>
    /// Informações nutricionais do alimento
    /// </summary>
    public NutritionalInfo NutritionalInfo { get; set; } = food.NutritionalInfo;
    
}