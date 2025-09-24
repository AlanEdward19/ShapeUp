using NutritionService.DailyMenu; // Adicionar para usar o DailyMenuDto
using System.Collections.Generic;
using System.Linq;

namespace NutritionService.UserNutrition;

/// <summary>
/// DTO para representar a nutrição completa do usuário.
/// </summary>
public class UserNutritionDto
{
    /// <summary>
    /// Identificador único da nutrição do usuário.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Identificador do usuário que criou a nutrição.
    /// </summary>
    public string CreatedBy { get; set; }
    
    /// <summary>
    /// Identificador do gerente de nutrição associado ao usuário.
    /// </summary>
    public string NutritionManagerId { get; set; }
    
    /// <summary>
    /// Identificador do usuário que utiliza os serviços de nutrição.
    /// </summary>
    public string UserId { get; set; }
    
    /// <summary>
    /// Lista de menus diários detalhados do usuário.
    /// </summary>
    public List<DailyMenuDto> DailyMenus { get; set; }

    /// <summary>
    /// Construtor para criar o DTO de UserNutrition.
    /// </summary>
    /// <param name="userNutrition">A entidade UserNutrition do banco de dados.</param>
    /// <param name="dailyMenus">A lista de DailyMenuDtos correspondentes ao plano do usuário.</param>
    public UserNutritionDto(UserNutrition userNutrition, List<DailyMenuDto> dailyMenus)
    {
        Id = userNutrition.Id;
        CreatedBy = userNutrition.CreatedBy;
        NutritionManagerId = userNutrition.NutritionManagerId;
        UserId = userNutrition.UserId;
        DailyMenus = dailyMenus; // Atribui a lista de DTOs já montada
    }
}