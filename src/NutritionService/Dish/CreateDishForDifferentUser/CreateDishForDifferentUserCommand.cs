namespace NutritionService.Dish.CreateDishForDifferentUser;

public class CreateDishForDifferentUserCommand
{
    public string Name { get; set; }
    public string[] FoodIds { get; set; }
    public string UserId { get; set; } = "";
    
    public CreateDishForDifferentUserCommand(string name, string[] foodIds)
    {
        Name = name;
        FoodIds  = foodIds;
    }

    public void SetUserId(string id)
    {
        UserId = id;
    }

}