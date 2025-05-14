using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.UserFood.Common;

public class Macronutrients(double protein, Carbohydrates carbohydrates, Fats fats)
{
    [BsonElement("proteins")]
    public double Proteins { get; private set; } = protein;
    
    [BsonElement("carbohydrates")]
    public Carbohydrates Carbohydrates { get; private set; } = carbohydrates;
    
    [BsonElement("fats")]
    public Fats Fats { get; private set; } = fats;
}