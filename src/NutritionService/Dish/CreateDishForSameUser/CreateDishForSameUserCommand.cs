namespace NutritionService.Dish.CreateDishForSameUser;


public class CreateDishForSameUserCommand
{
    public string Name { get; set; }
    
    // Alterado de string[] FoodIds para List<IngredientInput> Ingredients
    public List<IngredientInputForSameUser> Ingredients { get; set; }
    
    public CreateDishForSameUserCommand(string name, List<IngredientInputForSameUser> ingredients)
    {
        Name = name;
        Ingredients  = ingredients;
    }
}

public class IngredientInputForSameUser
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}