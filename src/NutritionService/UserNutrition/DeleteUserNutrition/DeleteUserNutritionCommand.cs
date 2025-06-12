namespace NutritionService.UserNutrition.DeleteUserNutrition;

public class DeleteUserNutritionCommand
{
    public string Id { get; set; }

    public DeleteUserNutritionCommand(string id)
    {
        Id = id;
    }
}