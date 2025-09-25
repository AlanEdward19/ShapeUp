namespace NutritionService.UserNutrition.ListUserNutritions;

/// <summary>
/// Query para buscar as nutrições de um usuário específico.
/// </summary>
public class ListUserNutritionsQuery
{
    public string UserId { get; set; }

    public ListUserNutritionsQuery()
    {
    }

    /// <summary>
    /// Define o Id do usuário para a consulta.
    /// </summary>
    /// <param name="userId"></param>
    public void SetUserId(string userId)
    {
        UserId = userId;
    }
}