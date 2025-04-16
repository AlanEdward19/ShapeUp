using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Common.Enums;

namespace NutritionService.Meal;

/// <summary>
/// Classe que representa uma refeição
/// </summary>
public class Meal
{
    /// <summary>
    /// Identificador da refeição
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";

    /// <summary>
    /// Tipo da refeição
    /// </summary>
    public MealType Type { get; set; } 
    /// <summary>
    /// Nome da refeição
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Lista de pratos que compoe a refeição
    /// </summary>
    public List<Dish.Dish> Dishes { get; set; } 
    /// <summary>
    /// Lista de alimentos que compoe a refeição
    /// </summary>
    public List<Food.Food> Foods { get; set; } 
    
    /// <summary>
    /// Construtor da classe Meal
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="dishes"></param>
    /// <param name="foods"></param>
    public Meal(MealType type, string name, List<Dish.Dish> dishes, List<Food.Food> foods)
    {
        Type = type;
        Name = name;
        Dishes = dishes;
        Foods = foods;
    }
    
    /// <summary>
    /// Método para gerar um novo ID para a refeição
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    /// <summary>
    /// Método para atualizar as informações da refeição
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="dishes"></param>
    /// <param name="foods"></param>
    public void UpdateInfo(MealType type, string name, List<Dish.Dish> dishes, List<Food.Food> foods)
    {
        Type = type;
        Name = name;
        Dishes = dishes;
        Foods = foods;
    }
}