using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Dish;

/// <summary>
/// Classe que representa um prato
/// </summary>
public class Dish
{
    /// <summary>
    /// Identificador do prato
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Nome do prato
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    ///Lista de ingredientes (alimentos) do prato
    /// </summary>
    public List<Food.Food> Ingredients { get; set; } = [];
}