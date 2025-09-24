using NutritionService.Meal; // Adicionar para usar o MealDto
using System.Collections.Generic;

namespace NutritionService.DailyMenu;

/// <summary>
/// DTO para representar um menu diário com todas as refeições detalhadas.
/// </summary>
public class DailyMenuDto
{
    /// <summary>
    /// Identificador único do menu diário.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Identificador do usuário que criou o menu diário.
    /// </summary>
    public string CreatedBy { get; set; }
    
    public string UserId { get; set; }
    
    /// <summary>
    /// Dia da semana do menu diário.
    /// </summary>
    public DayOfWeek? DayOfWeek { get; set; }
    
    /// <summary>
    /// Lista de refeições detalhadas do menu diário.
    /// </summary>
    public List<MealDto> Meals { get; set; }

    /// <summary>
    /// Construtor para criar o DTO do Menu Diário.
    /// </summary>
    /// <param name="dailyMenu">A entidade DailyMenu do banco de dados.</param>
    /// <param name="meals">A lista de MealDtos correspondentes às refeições do menu.</param>
    public DailyMenuDto(DailyMenu dailyMenu, List<MealDto> meals)
    {
        Id = dailyMenu.Id;
        CreatedBy = dailyMenu.CreatedBy;
        UserId = dailyMenu.UserId;
        DayOfWeek = dailyMenu.DayOfWeek;
        Meals = meals; // Atribui diretamente a lista de DTOs já pronta
    }
}