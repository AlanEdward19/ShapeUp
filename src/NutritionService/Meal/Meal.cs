using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Common.Enums;

namespace NutritionService.Meal;

/// <summary>
/// Classe que representa uma refeição
/// </summary>
public class Meal
{
    /// <summary>
    /// Identificador da refeição
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Tipo da refeição
    /// </summary>
    public MealType Type { get; set; } 
    /// <summary>
    /// Lista de pratos que compoe a refeição
    /// </summary>
    public List<Dish.Dish> Dishes { get; set; } = []; 
    /// <summary>
    /// Lista de alimentos que compoe a refeição
    /// </summary>
    public List<Food.Food> Foods { get; set; } = []; 
}