﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NutritionService.UserNutrition;

/// <summary>
/// Classe que representa a nutrição do usuário no banco de dados
/// </summary>
public class UserNutrition
{
    /// <summary>
    /// Identificador do gerenciamento de nutrição
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ""; 

    /// <summary>
    /// Identificador do perfil que criou o gerenciamento de nutrição
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    /// ID do responsável pela nutrição
    /// </summary>
    public string NutritionManagerId { get; set; }
    
    /// <summary>
    /// Lista de cardápios diários do usuário
    /// </summary>
    public List<DailyMenu.DailyMenu> DailyMenus { get; set; } 
    
    public void SetId()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }
    
    /// <summary>
    /// Método para definir o identificador do perfil que criou o gerenciamento de nutrição
    /// </summary>
    /// <param name="createdBy"></param>
    public void SetCreatedBy(string createdBy)
    {
        CreatedBy = createdBy;
    }
    
    public void UpdateInfo(string nutritionManagerId, List<DailyMenu.DailyMenu> dailyMenus)
    {
        DailyMenus = dailyMenus;
        NutritionManagerId = nutritionManagerId;
    }

    public UserNutrition(string nutritionManagerId, List<DailyMenu.DailyMenu> dailyMenus)
    {
        NutritionManagerId = nutritionManagerId;
        DailyMenus = dailyMenus;
    }
    
}
