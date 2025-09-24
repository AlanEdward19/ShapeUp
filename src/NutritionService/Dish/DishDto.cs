using NutritionService.Common;

namespace NutritionService.Dish;

/// <summary>
/// DTO para representar um prato com todos os detalhes de seus ingredientes.
/// </summary>
public class DishDto
{
    /// <summary>
    /// Identificador único do prato.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Identificador do usuário que criou o prato.
    /// </summary>
    public string CreatedBy { get; set; }
    
    public string UserId { get; set; }
    
    /// <summary>
    /// Nome do prato.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Lista de ingredientes detalhados do prato.
    /// </summary>
    public List<DishIngredientDto> Ingredients { get; set; }

    /// <summary>
    /// Construtor para criar o DTO do Prato.
    /// </summary>
    /// <param name="dish">A entidade Dish do banco de dados.</param>
    /// <param name="foods">A lista de Foods correspondentes aos ingredientes do prato.</param>
    public DishDto(Dish dish, List<Food> foods)
    {
        Id = dish.Id;
        CreatedBy = dish.CreatedBy;
        UserId = dish.UserId;
        Name = dish.Name;

        // Mapeia os alimentos (foods) para os ingredientes do prato, combinando com a quantidade.
        var foodMap = foods.ToDictionary(f => f.Id);
        
        Ingredients = dish.Ingredients
            .Where(i => foodMap.ContainsKey(i.FoodId)) // Garante que o alimento foi encontrado
            .Select(ingredient => new DishIngredientDto(
                foodMap[ingredient.FoodId].Clone(), // Pega o alimento do mapa e clona
                ingredient.Quantity
            )).ToList();
    }
}

/// <summary>
/// DTO para representar um ingrediente dentro de um prato,
/// combinando os detalhes do alimento com sua quantidade.
/// </summary>
public class DishIngredientDto
{
    /// <summary>
    /// A quantidade do ingrediente.
    /// </summary>
    public double Quantity { get; set; }
    
    /// <summary>
    /// O objeto completo do alimento.
    /// </summary>
    public Food Food { get; set; }

    public DishIngredientDto(Food food, double quantity)
    {
        Food = food;
        Quantity = quantity;
    }
}