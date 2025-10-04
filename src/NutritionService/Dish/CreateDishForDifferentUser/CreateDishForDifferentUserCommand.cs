namespace NutritionService.Dish.CreateDishForDifferentUser;

// Reutilize ou defina a classe IngredientInput aqui
public class IngredientInputForDifferentUser
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}

public class CreateDishForDifferentUserCommand
{
    public string Name { get; set; }
    public List<IngredientInputForDifferentUser> Ingredients { get; set; }
    public string UserId { get; private set; } = "";
    
    public CreateDishForDifferentUserCommand(string name, List<IngredientInputForDifferentUser> ingredients)
    {
        Name = name;
        Ingredients  = ingredients;
    }

    public void SetUserId(string id)
    {
        UserId = id;
    }
}