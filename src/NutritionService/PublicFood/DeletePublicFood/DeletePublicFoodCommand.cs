namespace NutritionService.PublicFood.DeletePublicFood;

public class DeletePublicFoodCommand
{
    public string Id { get; private set; }
    
    public DeletePublicFoodCommand(string id)
    {
        Id = id;
    }
}