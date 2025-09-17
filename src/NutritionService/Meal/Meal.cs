using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Common;
using NutritionService.Meal.Common.Enums;
using NutritionService.UserFood;

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
    /// Identificador do perfil que criou a refeição
    /// </summary>
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";
    
    /// <summary>
    /// Identificador do perfil que utiliza a refeicao
    /// </summary>
    [BsonElement("userId")]
    public string UserId { get; private set; } = "";
    
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
    /// Lista de alimentos que compõe a refeição
    /// </summary>
    public List<Food> Foods { get; set; } 
    
    /// <summary>
    /// Construtor da classe Meal
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="dishes"></param>
    /// <param name="foods"></param>
    public Meal(MealType type, string name, List<Dish.Dish> dishes, List<Food> foods)
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
    /// Método para definir o identificador do perfil que criou a refeição
    /// </summary>
    /// <param name="createdBy"></param>
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }
    
    /// <summary>
    /// Método para definir o identificador do perfil que vai utilizar a refeicao
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Método para atualizar as informações da refeição
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <param name="dishes"></param>
    /// <param name="foods"></param>
    public void UpdateInfo(MealType type, string name, List<Dish.Dish> dishes, List<Food> foods)
    {
        Type = type;
        Name = name;
        Dishes = dishes;
        Foods = foods;
    }
    
    /// <summary>
    /// Clona a refeição atual, criando uma nova instância com os mesmos valores.
    /// </summary>
    /// <returns></returns>
    public Meal Clone()
    {
        return new Meal(Type, Name, Dishes.Select(d => d.Clone()).ToList(), Foods.Select(f => f.Clone()).ToList())
        {
            Id = Id,
            CreatedBy = CreatedBy
        };
    }
}