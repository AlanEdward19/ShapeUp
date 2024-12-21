using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Food.Common;

namespace NutritionService.Food;

public class Food(string name, string brand, string barCode, NutritionalInfo nutritionalInfo)
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; private set; }
    
    [BsonElement("name")]
    public string Name { get; private set; } = name;
    
    [BsonElement("brand")]
    public string Brand { get; private set; } = brand;
    
    [BsonElement("barCode")]
    public string BarCode { get; private set; } = barCode;
    
    [BsonElement("revised")]
    public bool Revised { get; private set; } = false;
    
    [BsonElement("nutritionalInfo")]
    public NutritionalInfo NutritionalInfo { get; private set; } = nutritionalInfo;

    public void MarkAsRevised()
    {
        Revised = true;
    }
    
    public void GenerateId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
}