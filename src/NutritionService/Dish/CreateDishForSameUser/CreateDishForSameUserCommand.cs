namespace NutritionService.Dish.CreateDishForSameUser;


public class CreateDishForSameUserCommand
{
    public string Name { get; set; }
    
    // Alterado de string[] FoodIds para List<IngredientInput> Ingredients
    public List<IngredientInput> Ingredients { get; set; }
    
    public CreateDishForSameUserCommand(string name, List<IngredientInput> ingredients)
    {
        Name = name;
        Ingredients  = ingredients;
    }
}

public class IngredientInput
{
    public string FoodId { get; set; }
    public double Quantity { get; set; }
}