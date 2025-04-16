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
    public string Id { get; private set; } =  "";

    /// <summary>
    /// Nome do prato
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///Lista de ingredientes (alimentos) do prato
    /// </summary>
    public List<Food.Food> Ingredients { get; set; }
    
    public Dish(string name, List<Food.Food> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
    
    /// <summary>
    /// Método para gerar um novo ID para o prato
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    public void UpdateInfo(string name, List<Food.Food> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
    
}