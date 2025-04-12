using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.DailyMenu;

/// <summary>
/// Classe que representa um cardápio diário
/// </summary>
public class DailyMenu
{
    /// <summary>
    /// Identiicador do cardápio
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// 
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public List<Meal.Meal> Meals { get; set; } = new();
}