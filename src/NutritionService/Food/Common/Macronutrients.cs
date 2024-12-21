using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class Macronutrients(double protein, Carbohydrates carbohydrates, Fats fats)
{
    [BsonElement("protein")]
    public double Protein { get; private set; } = protein;
    
    [BsonElement("carbohydrates")]
    public Carbohydrates Carbohydrates { get; private set; } = carbohydrates;
    
    [BsonElement("fats")]
    public Fats Fats { get; private set; } = fats;
}