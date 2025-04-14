using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Common;

namespace NutritionService.Dish;

/// <summary>
/// Classe que representa um prato
/// </summary>
public class Dish(string name, string? brand, string? barCode, NutritionalInfo? nutritionalInfo, List<Food.Food> ingredients)
{
    /// <summary>
    /// Identificador do prato
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    /// <summary>
    /// Nome do prato
    /// </summary>
    public string Name { get; set; } = name;
    
    /// <summary>
    /// Marca do prato
    /// </summary>
    [BsonElement("brand")]
    [BsonIgnoreIfNull]
    public string? Brand { get; private set; } = brand;
    
    /// <summary>
    /// Còdigo de barras do prato
    /// </summary>
    [BsonElement("barCode")]
    [BsonIgnoreIfNull]
    public string? BarCode { get; private set; } = barCode;
    
    /// <summary>
    /// Status de revisão do prato
    /// </summary>
    [BsonElement("revised")]
    public bool Revised { get; private set; } = false;
    
    /// <summary>
    /// Informações nutricionais do prato
    /// </summary>
    [BsonElement("nutritionalInfo")]
    [BsonIgnoreIfNull]
    public NutritionalInfo? NutritionalInfo { get; private set; } = nutritionalInfo;
    

    /// <summary>
    /// Método para assinalar o prato como revisado
    /// </summary>
    public void MarkAsRevised()
    {
        Revised = true;
    }
    
    /// <summary>
    ///Lista de ingredientes (alimentos) do prato
    /// </summary>
    public List<Food.Food> Ingredients { get; set; } = ingredients;
    
    /// <summary>
    /// Método para gerar um novo ID para o prato
    /// </summary>
    public void GenerateId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    public void UpdateInfo(string name, List<Food.Food> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
    
}