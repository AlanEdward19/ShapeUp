using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace NutritionService.UserNutrition;

/// <summary>
/// Classe que representa a nutrição do usuário no banco de dados.
/// </summary>
public class UserNutrition
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ""; 

    public string CreatedBy { get; set; } = "";

    /// <summary>
    /// ID do responsável pelo gerenciamento da nutrição.
    /// </summary>
    public string NutritionManagerId { get; set; }
    
    [BsonElement("userId")]
    public string UserId { get; private set; } = "";
    
    /// <summary>
    /// Lista de IDs dos cardápios diários do usuário.
    /// </summary>
    public List<string> DailyMenuIds { get; set; } 
    
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
    /// Atualiza as informações do plano de nutrição do usuário.
    /// </summary>
    public void UpdateInfo(string nutritionManagerId, List<string> dailyMenuIds)
    {
        DailyMenuIds = dailyMenuIds;
        NutritionManagerId = nutritionManagerId;
    }

    /// <summary>
    /// Construtor da classe UserNutrition.
    /// </summary>
    public UserNutrition(string nutritionManagerId, List<string> dailyMenuIds)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenuIds = dailyMenuIds;
    }
}