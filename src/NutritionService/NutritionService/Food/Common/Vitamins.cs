using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class Vitamins
{
    [BsonElement("vitaminD")]
    public double VitaminD { get; private set; }
    
    [BsonElement("vitaminA")]
    public double VitaminA { get; private set; }
    
    [BsonElement("vitaminC")]
    public double VitaminC { get; private set; }

    public Vitamins(double? vitaminD, double? vitaminA, double? vitaminC)
    {
        VitaminD = vitaminD ?? 0;
        VitaminA = vitaminA ?? 0;
        VitaminC = vitaminC ?? 0;
    }

    public Vitamins()
    {
        VitaminD = 0;
        VitaminA = 0;
        VitaminC = 0;
    }
}