using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Common;
using NutritionService.UserFood;

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
    /// Identificador do perfil que criou o prato
    /// </summary>
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";

    /// <summary>
    /// Nome do prato
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///Lista de ingredientes (alimentos) do prato
    /// </summary>
    public List<Food> Foods { get; set; }
    
    public Dish(string name, List<Food> foods)
    {
        Name = name;
        Foods = foods;
    }
    
    /// <summary>
    /// Método para gerar um novo ID para o prato
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    
    /// <summary>
    /// Método para definir o identificador do perfil que criou o prato
    /// </summary>
    /// <param name="createdBy"></param>
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }
    
    public void UpdateInfo(string name, List<Food> foods)
    {
        Name = name;
        Foods = foods;
    }
    
    /// <summary>
    /// Método para clonar o prato
    /// </summary>
    /// <returns></returns>
    public Dish Clone()
    {
        return new Dish(Name, Foods.Select(f => f.Clone()).ToList())
        {
            Id = Id,
            CreatedBy = CreatedBy
        };
    }
}