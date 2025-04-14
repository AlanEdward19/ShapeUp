using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class Micronutrients(double cholesterol, double sodium, double calcium, double iron, double potassium)
{
    [BsonElement("sodium")] public double Sodium { get; private set; } = sodium;

    [BsonElement("calcium")] public double Calcium { get; private set; } = calcium;

    [BsonElement("iron")] public double Iron { get; private set; } = iron;

    [BsonElement("potassium")] public double Potassium { get; private set; } = potassium;
}