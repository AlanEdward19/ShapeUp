// /src/NutritionService/Meal/Meal.cs

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Dish; 
using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal;

/// <summary>
/// Classe que representa uma refeição
/// </summary>
public class Meal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";
    
    [BsonElement("userId")]
    public string UserId { get; private set; } = "";
    
    public MealType Type { get; set; } 
    public string Name { get; set; }
    
    /// <summary>
    /// Lista de IDs dos pratos que compoem a refeição
    /// </summary>
    public List<string> DishIds { get; set; } 
    
    /// <summary>
    /// Lista de ingredientes (alimentos avulsos) que compõe a refeição
    /// </summary>
    public List<Ingredient> Ingredients { get; set; } 
    
    /// <summary>
    /// Construtor da classe Meal
    /// </summary>
    public Meal(MealType type, string name, List<string> dishIds, List<Ingredient> ingredients)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        Ingredients = ingredients;
    }
    
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }

    public void SetUserId(string userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Método para atualizar as informações da refeição
    /// </summary>
    public void UpdateInfo(MealType type, string name, List<string> dishIds, List<Ingredient> ingredients)
    {
        Type = type;
        Name = name;
        DishIds = dishIds;
        Ingredients = ingredients;
    }
}