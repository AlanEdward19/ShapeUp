using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Common;

public class Macronutrients(double? proteins, Carbohydrates? carbohydrates, Fats? fats)
{
    [BsonElement("proteins")]
    public double? Proteins { get; private set; } = proteins;
    
    [BsonElement("carbohydrates")]
    public Carbohydrates? Carbohydrates { get; private set; } = carbohydrates;
    
    [BsonElement("fats")]
    public Fats? Fats { get; private set; } = fats;

    public Macronutrients? Clone()
    {
        return new Macronutrients(
            Proteins,
            Carbohydrates?.Clone(),
            Fats?.Clone()
        );
    }
}