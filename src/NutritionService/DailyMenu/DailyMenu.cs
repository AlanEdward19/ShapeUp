using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace NutritionService.DailyMenu;

/// <summary>
/// Classe que representa um cardápio diário.
/// </summary>
public class DailyMenu
{
    /// <summary>
    /// Identificador do cardápio.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = "";
    
    /// <summary>
    /// Identificador do perfil que criou o cardápio.
    /// </summary>
    [BsonElement("createdBy")]
    public string CreatedBy { get; private set; } = "";
    
    /// <summary>
    /// Identificador do perfil que utiliza o cardápio.
    /// </summary>
    [BsonElement("userId")]
    public string UserId { get; private set; } = "";

    /// <summary>
    /// Dia da Semana.
    /// </summary>
    public DayOfWeek? DayOfWeek { get; private set; }
    
    /// <summary>
    /// Lista de IDs das refeições do dia.
    /// </summary>
    public List<string> MealIds { get; private set; } = [];

    /// <summary>
    /// Construtor da classe DailyMenu.
    /// </summary>
    public DailyMenu(DayOfWeek? dayOfWeek, List<string> mealIds)
    {
        DayOfWeek = dayOfWeek;
        MealIds = mealIds;
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
    /// Método para atualizar as informações do cardápio diário.
    /// </summary>
    public void UpdateInfo(DayOfWeek? dayOfWeek, List<string> mealIds)
    {
        DayOfWeek = dayOfWeek;
        MealIds = mealIds;
    }
}