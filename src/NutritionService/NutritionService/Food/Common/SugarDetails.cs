using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class SugarDetails
{
    [BsonElement("total")]
    public double Total { get; private set; }
    
    [BsonElement("addedSugar")]
    public double AddedSugar { get; private set; }
    
    [BsonElement("sugarAlcohols")]
    public double SugarAlcohols { get; private set; }
    
    public SugarDetails(double total, double? addedSugar, double? sugarAlcohols)
    {
        Total = total;
        AddedSugar = addedSugar ?? 0;
        SugarAlcohols = sugarAlcohols ?? 0;
    }

    public SugarDetails()
    {
        Total = 0;
        AddedSugar = 0;
        SugarAlcohols = 0;
    }
}