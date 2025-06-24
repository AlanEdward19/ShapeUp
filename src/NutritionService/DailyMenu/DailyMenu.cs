using System.Diagnostics.Metrics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.DailyMenu;

/// <summary>
/// Classe que representa um cardápio diário
/// </summary>
public class DailyMenu
{
    /// <summary>
    /// Identificador do cardápio
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = "";
    
    /// <summary>
    /// Identificador do perfil que criou o cardápio
    /// </summary>
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";

    /// <summary>
    /// Dia da Semana
    /// </summary>
    public DayOfWeek? DayOfWeek { get; private set; }
    /// <summary>
    /// Lista de refeições do dia
    /// </summary>
    public List<Meal.Meal> Meals { get; private set; } = [];

    /// <summary>
    /// Construtor da classe DailyMenu
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <param name="meals"></param>
    public DailyMenu(DayOfWeek? dayOfWeek, List<Meal.Meal> meals)
    {
        DayOfWeek = dayOfWeek;
        Meals = meals;
    }

    /// <summary>
    /// Método para gerar um novo ID para o cardápio
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    /// <summary>
    /// Método para definir o identificador do perfil que criou o cardápio
    /// </summary>
    /// <param name="createdBy"></param>
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }

    /// <summary>
    /// Método para atualizar as informações do cardápio diário
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <param name="meals"></param>
    public void UpdateInfo(DayOfWeek? dayOfWeek, List<Meal.Meal> meals)
    {
        DayOfWeek = dayOfWeek;
        Meals = meals;
    }
    
    /// <summary>
    /// Método para clonar o cardápio diário
    /// </summary>
    /// <returns></returns>
    public DailyMenu Clone()
    {
        return new DailyMenu(DayOfWeek, Meals.Select(m => m.Clone()).ToList())
        {
            Id = Id,
            CreatedBy = CreatedBy
        };
    }
}