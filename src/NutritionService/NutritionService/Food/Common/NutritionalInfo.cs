using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Food.Common;

public class NutritionalInfo(Macronutrients macronutrients, Micronutrients? micronutrients, Vitamins? vitamins)
{
    [BsonElement("macronutrients")]
    public Macronutrients Macronutrients { get; private set; } = macronutrients;
    
    [BsonElement("micronutrients")]
    public Micronutrients Micronutrients { get; private set; } = micronutrients ?? new Micronutrients();
    
    [BsonElement("vitamins")]
    public Vitamins Vitamins { get; private set; } = vitamins ?? new Vitamins();
}