using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Common;

public class MicronutrientDetails(double quantity, string unit)
{
    [BsonElement("quantity")] public double Quantity { get; private set; } = quantity;
    [BsonElement("unit")] public string Unit { get; private set; } = unit;
}