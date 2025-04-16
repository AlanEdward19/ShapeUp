using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Food.Common;

namespace NutritionService.Food;

/// <summary>
/// Classe que representa um alimento
/// </summary>
/// <param name="name"></param>
/// <param name="brand"></param>
/// <param name="barCode"></param>
/// <param name="nutritionalInfo"></param>
public class Food
{
    /// <summary>
    /// Identitificador do alimento
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; private set; } = "";
    
    /// <summary>
    /// Nome do alimento
    /// </summary>
    [BsonElement("name")]
    public string Name { get; private set; }
    
    /// <summary>
    /// Marca do alimento
    /// </summary>
    [BsonElement("brand")]
    [BsonIgnoreIfNull]
    public string? Brand { get; private set; }
    
    /// <summary>
    /// Còdigo de barras do alimento
    /// </summary>
    [BsonElement("barCode")]
    [BsonIgnoreIfNull]
    public string? BarCode { get; private set; }
    
    /// <summary>
    /// Status de revisão do alimento
    /// </summary>
    [BsonElement("revised")]
    public bool Revised { get; private set; } = false;
    
    /// <summary>
    /// Informações nutricionais do alimento
    /// </summary>
    [BsonElement("nutritionalInfo")]
    public NutritionalInfo NutritionalInfo { get; private set; }
    
    /// <summary>
    /// Construtor da classe Food
    /// </summary>
    /// <param name="name"></param>
    /// <param name="brand"></param>
    /// <param name="barCode"></param>
    /// <param name="nutritionalInfo"></param>
    public Food(string name, string? brand, string? barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
    

    /// <summary>
    /// Método para assinalar o alimento como revisado
    /// </summary>
    public void MarkAsRevised()
    {
        Revised = true;
    }
    
    /// <summary>
    /// Método para gerar um novo identificador para o alimento
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    /// <summary>
    /// Método para atualizar as informações do alimento
    /// </summary>
    /// <param name="name"></param>
    /// <param name="brand"></param>
    /// <param name="barCode"></param>
    /// <param name="nutritionalInfo"></param>
    public void UpdateInfo(string name, string brand, string barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }
}