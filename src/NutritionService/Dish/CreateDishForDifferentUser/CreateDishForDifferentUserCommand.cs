namespace NutritionService.Dish.CreateDishForDifferentUser;

// Reutilize ou defina a classe IngredientInput aqui
public class IngredientInput
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}

public class CreateDishForDifferentUserCommand
{
    public string Name { get; set; }
    public List<IngredientInput> Ingredients { get; set; }
    public string UserId { get; private set; } = "";
    
    public CreateDishForDifferentUserCommand(string name, List<IngredientInput> ingredients)
    {
        Name = name;
        Ingredients  = ingredients;
    }

    public void SetUserId(string id)
    {
        UserId = id;
    }
}