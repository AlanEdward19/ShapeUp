using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class Carbohydrates(double total, double dietaryFiber, SugarDetails sugar)
{
    [BsonElement("total")]
    public double Total { get; private set; } = total;
    
    [BsonElement("dietaryFiber")]
    public double DietaryFiber { get; private set; } = dietaryFiber;

    [BsonElement("sugar")] public SugarDetails Sugar { get; private set; } = sugar;
}