// /src/NutritionService/Dish/Dish.cs

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NutritionService.Common; // Food ainda pode ser usado em DTOs, então mantemos a referência comum.

namespace NutritionService.Dish;

/// <summary>
/// Classe que representa um prato.
/// </summary>
public class Dish
{
    /// <summary>
    /// Identificador do prato.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } =  "";
    
    /// <summary>
    /// Identificador do perfil que criou o prato.
    /// </summary>
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";
    
    /// <summary>
    /// Identificador do perfil que utiliza o cardápio.
    /// </summary>
    [BsonElement("userId")]
    public string UserId { get; private set; } = "";

    /// <summary>
    /// Nome do prato.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Lista de ingredientes do prato.
    /// </summary>
    public List<Ingredient> Ingredients { get; set; }

    /// <summary>
    /// Construtor para criar um novo prato.
    /// </summary>
    public Dish(string name, List<Ingredient> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
    
    /// <summary>
    /// Método para gerar um novo ID para o prato.
    /// </summary>
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    /// <summary>
    /// Método para definir o identificador do perfil que criou o prato.
    /// </summary>
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }
    
    /// <summary>
    /// Método para definir o identificador do perfil que vai utilizar o cardápio.
    /// </summary>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Atualiza as informações do prato.
    /// </summary>
    public void UpdateInfo(string name, List<Ingredient> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
    
    
}

// /src/NutritionService/Dish/Ingredient.cs


/// <summary>
/// Representa um ingrediente dentro de um prato, com referência ao alimento e sua quantidade.
/// </summary>
public class Ingredient
{
    /// <summary>
    /// Identificador do alimento (Food).
    /// </summary>
    public string FoodId { get; set; }

    /// <summary>
    /// A quantidade do ingrediente. Pode ser em unidades, gramas, ml, etc.
    /// </summary>
    public double Quantity { get; set; }
    
    /// <summary>
    /// Construtor para criar um novo ingrediente.
    /// </summary>
    public Ingredient(string foodId, double quantity)
    {
        FoodId = foodId;
        Quantity = quantity;
    }
    
    /// <summary>
    /// Método para clonar o ingrediente.
    /// </summary>
    public Ingredient Clone()
    {
        return new Ingredient(FoodId, Quantity);
    }
}