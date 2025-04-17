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
    /// Dia da Semana
    /// </summary>
    public DayOfWeek? DayOfWeek { get; private set; } = DateTime.Now.DayOfWeek;
    /// <summary>
    /// Lista de refeições do dia
    /// </summary>
    public List<Meal.Meal> Meals { get; private set; } = [];

    public DailyMenu(DayOfWeek? dayOfWeek, List<Meal.Meal> meals)
    {
        DayOfWeek = dayOfWeek;
        Meals = meals;
    }

    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    public void UpdateInfo(DayOfWeek? dayOfWeek, List<Meal.Meal> meals)
    {
        DayOfWeek = dayOfWeek;
        Meals = meals;
    }
}