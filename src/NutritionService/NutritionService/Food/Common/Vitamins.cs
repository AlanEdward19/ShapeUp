using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class Vitamins(double vitaminD, double vitaminA, double vitaminC)
{
    [BsonElement("vitaminD")] public double VitaminD { get; private set; } = vitaminD;

    [BsonElement("vitaminA")] public double VitaminA { get; private set; } = vitaminA;

    [BsonElement("vitaminC")] public double VitaminC { get; private set; } = vitaminC;
}