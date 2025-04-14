using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.Common;

/// <summary>
/// Classe que representa as informações nutricionais de um alimento
/// </summary>
/// <param name="macronutrients"></param>
/// <param name="micronutrients"></param>
/// <param name="vitamins"></param>
/// <param name="totalMass"></param>
/// <param name="servingSize"></param>
/// <param name="calories"></param>
public class NutritionalInfo(Macronutrients macronutrients, Dictionary<string, MicronutrientDetails> micronutrients,
    double servingSize, double calories)
{
    /// <summary>
    /// Macronutrientes do alimento
    /// </summary>
    [BsonElement("macronutrients")]
    public Macronutrients Macronutrients { get; private set; } = macronutrients;

    /// <summary>
    /// Micronutrientes por porção do alimento
    /// </summary>
    [BsonElement("micronutrients")] public Dictionary<string, MicronutrientDetails> Micronutrients { get; private set; } = micronutrients;

    /// <summary>
    /// Massa por porção do alimento
    /// </summary>
    [BsonElement("servingSize")] public double ServingSize { get; private set; } = servingSize;

    /// <summary>
    /// Calorias por porção do alimento
    /// </summary>
    [BsonElement("calories")] public double Calories { get; private set; } = calories;
}