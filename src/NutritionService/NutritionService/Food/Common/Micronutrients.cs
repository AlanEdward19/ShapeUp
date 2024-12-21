using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class Micronutrients
{
    [BsonElement("cholesterol")]
    public double Cholesterol { get; private set; }
    
    [BsonElement("sodium")]
    public double Sodium { get; private set; }
    
    [BsonElement("calcium")]
    public double Calcium { get; private set; }
    
    [BsonElement("iron")]
    public double Iron { get; private set; }
    
    [BsonElement("potassium")]
    public double Potassium { get; private set; }

    public Micronutrients(double? cholesterol, double? sodium, double? calcium, double? iron, double? potassium)
    {
        Cholesterol = cholesterol ?? 0;
        Sodium = sodium ?? 0;
        Calcium = calcium ?? 0;
        Iron = iron ?? 0;
        Potassium = potassium ?? 0;
    }

    public Micronutrients()
    {
        Cholesterol = 0;
        Sodium = 0;
        Calcium = 0;
        Iron = 0;
        Potassium = 0;
    }
}