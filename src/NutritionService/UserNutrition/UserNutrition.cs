using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.UserNutrition;

/// <summary>
/// Classe que representa a nutrição do usuário no banco de dados
/// </summary>
public class UserNutrition
{
    /// <summary>
    /// Identificador do usuário no banco mongo
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ""; // ID do usuário

    /// <summary>
    /// Identificador do usuário no banco externo
    /// </summary>
    public string UserId { get; set; } // ID do usuário

    public string NutritionManagerId { get; set; } // ID do responsável pela nutrição
    
    /// <summary>
    /// Lista de cardápios diários do usuário
    /// </summary>
    public List<DailyMenu.DailyMenu> DailyMenus { get; set; } = [];
    
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    public void UpdateInfo(string nutritionManagerId, List<DailyMenu.DailyMenu> dailyMenus)
    {
        DailyMenus = dailyMenus;
        NutritionManagerId = nutritionManagerId;
    }

    public UserNutrition(string userId, string nutritionManagerId, List<DailyMenu.DailyMenu> dailyMenus)
    {
        UserId = userId;
        NutritionManagerId = nutritionManagerId;
        DailyMenus = dailyMenus;
    }
    
}
