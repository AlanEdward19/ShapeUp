using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Common;

/// <summary>
/// Classe que representa os detalhes de um micronutriente
/// </summary>
/// <param name="quantity"></param>
/// <param name="unit"></param>
public class MicronutrientDetails(double quantity, string unit)
{
    [BsonElement("quantity")] public double Quantity { get; private set; } = quantity;
    [BsonElement("unit")] public string Unit { get; private set; } = unit;

    public MicronutrientDetails Clone()
    {
        return new MicronutrientDetails(Quantity, Unit);
    }
}