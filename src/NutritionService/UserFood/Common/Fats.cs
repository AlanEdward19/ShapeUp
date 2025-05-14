using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.UserFood.Common;

public class Fats(
    double total,
    double saturatedFat,
    double transFat,
    double polyunsaturatedFat,
    double monounsaturatedFat,
    double cholesterol)
{
    [BsonElement("total")]
    public double Total { get; private set; } = total;
    
    [BsonElement("saturatedFat")]
    public double SaturatedFat { get; private set; } = saturatedFat;
    
    [BsonElement("transFat")]
    public double TransFat { get; private set; } = transFat;
    
    [BsonElement("polyunsaturatedFat")]
    public double PolyunsaturatedFat { get; private set; } = polyunsaturatedFat;
    
    [BsonElement("monounsaturatedFat")]
    public double MonounsaturatedFat { get; private set; } = monounsaturatedFat;
    
    [BsonElement("cholesterol")]
    public double Cholesterol { get; private set; } = cholesterol;
}