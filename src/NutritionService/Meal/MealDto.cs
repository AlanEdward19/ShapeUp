using NutritionService.Common;
using NutritionService.Dish; // Precisamos do DishDto e DishIngredientDto
using NutritionService.Meal.Common.Enums;

namespace NutritionService.Meal;

/// <summary>
/// DTO para representar uma refeição com todos os seus componentes detalhados.
/// </summary>
public class MealDto
{
    public string Id { get; set; }
    public string CreatedBy { get; set; }
    public string UserId { get; set; }
    public MealType Type { get; set; }
    public string Name { get; set; }
    
    /// <summary>
    /// Lista de pratos detalhados que compõem a refeição.
    /// </summary>
    public List<DishDto> Dishes { get; set; }
    
    /// <summary>
    /// Lista de ingredientes avulsos (alimentos) detalhados.
    /// </summary>
    public List<DishIngredientDto> Ingredients { get; set; }

    /// <summary>
    /// Construtor para criar o DTO da Refeição.
    /// </summary>
    /// <param name="meal">A entidade Meal do banco de dados.</param>
    /// <param name="dishes">A lista de Dish DTOs correspondentes aos pratos da refeição.</param>
    /// <param name="foods">A lista de Foods correspondentes aos ingredientes avulsos da refeição.</param>
    public MealDto(Meal meal, List<DishDto> dishes, List<Food> foods)
    {
        Id = meal.Id;
        CreatedBy = meal.CreatedBy;
        UserId = meal.UserId;
        Type = meal.Type;
        Name = meal.Name;
        Dishes = dishes; // A lista de DishDtos já vem pronta

        // Mapeia os alimentos (foods) para os ingredientes da refeição, combinando com a quantidade.
        var foodMap = foods.ToDictionary(f => f.Id);
        
        Ingredients = meal.Ingredients
            .Where(i => foodMap.ContainsKey(i.FoodId)) // Garante que o alimento foi encontrado
            .Select(ingredient => new DishIngredientDto(
                foodMap[ingredient.FoodId].Clone(), // Pega o alimento do mapa e clona
                ingredient.Quantity
            )).ToList();
    }
}