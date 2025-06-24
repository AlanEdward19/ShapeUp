using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Common;

/// <summary>
/// Classe que representa um alimento
/// </summary>
public class Food
{
    /// <summary>
    /// Identitificador do alimento
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; private set; } = "";

    /// <summary>
    /// Identificador do perfil que criou o alimento
    /// </summary>
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";
    
    /// <summary>
    /// Nome do alimento
    /// </summary>
    [BsonElement("name")]
    public string Name { get; private set; }
    
    /// <summary>
    /// Marca do alimento
    /// </summary>
    [BsonElement("brand")]
    public string? Brand { get; private set; }
    
    /// <summary>
    /// Código de barras do alimento
    /// </summary>
    [BsonElement("barCode")]
    public string? BarCode { get; private set; }
    
    /// <summary>
    /// Classificação de revisão do alimento
    /// </summary>
    [BsonElement("revised")]
    public bool IsRevised { get; private set; } = false;
    
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
        IsRevised = true;
    }
    
    /// <summary>
    /// Método para gerar um novo identificador para o alimento
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    /// <summary>
    /// Método para definir o identificador do perfil que criou o alimento
    /// </summary>
    /// <param name="createdBy"></param>
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }

    /// <summary>
    /// Método para atualizar as informações do alimento
    /// </summary>
    /// <param name="name"></param>
    /// <param name="brand"></param>
    /// <param name="barCode"></param>
    /// <param name="nutritionalInfo"></param>
    public void UpdateInfo(string name, string? brand, string? barCode, NutritionalInfo nutritionalInfo)
    {
        Name = name;
        Brand = brand;
        BarCode = barCode;
        NutritionalInfo = nutritionalInfo;
    }

    /// <summary>
    /// Método para clonar o objeto Food
    /// </summary>
    /// <returns></returns>
    public Food Clone()
    {
        return new Food(Name, Brand, BarCode, NutritionalInfo.Clone());
    }
}