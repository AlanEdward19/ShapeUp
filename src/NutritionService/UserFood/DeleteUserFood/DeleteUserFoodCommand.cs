namespace NutritionService.UserFood.DeleteUserFood;

public class DeleteUserFoodCommand
{
    public string Id { get; private set; }
    
    public DeleteUserFoodCommand(string id)
    {
        Id = id;
    }
}