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
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    /// <summary>
    /// Identificador do usuário no banco externo
    /// </summary>
    public string UserId { get; set; } // ID do usuário
    /// <summary>
    /// Lista de cardápios diários do usuário
    /// </summary>
    public List<DailyMenu.DailyMenu> DailyMenus { get; set; } = [];
}
