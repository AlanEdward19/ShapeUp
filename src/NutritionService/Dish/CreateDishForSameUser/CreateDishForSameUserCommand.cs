namespace NutritionService.Dish.CreateDishForSameUser;

public class CreateDishForSameUserCommand
{
    public string Name { get; set; }
    public string[] FoodIds { get; set; }
    
    public CreateDishForSameUserCommand(string name, string[] foodIds)
    {
        Name = name;
        FoodIds  = foodIds;
    }
    
    
}