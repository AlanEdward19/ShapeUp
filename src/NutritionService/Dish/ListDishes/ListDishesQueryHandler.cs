using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository; // 1. Adicionar o using para o repositório de alimentos

namespace NutritionService.Dish.ListDishes;

/// <summary>
/// ListDishesQueryHandler handles the query to list dishes.
/// </summary>
public class ListDishesQueryHandler : IHandler<IEnumerable<DishDto>, ListDishesQuery>
{
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository; // 2. Declarar o novo repositório

    /// <summary>
    /// Construtor com injeção de dependência para os repositórios.
    /// </summary>
    public ListDishesQueryHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository foodRepository) // 3. Injetar os dois repositórios
    {
        _dishRepository = dishRepository;
        _foodRepository = foodRepository;
    }

    /// <summary>
    /// Handles the retrieval of dishes based on pagination parameters.
    /// </summary>
    public async Task<IEnumerable<DishDto>> HandleAsync(ListDishesQuery item, CancellationToken cancellationToken)
    {
        // ETAPA A: Buscar pratos paginados
        var dishes = (await _dishRepository.ListDishesAsync(item.Page, item.Rows, cancellationToken, item.UserId)).ToList();

        if (!dishes.Any())
            return Enumerable.Empty<DishDto>();

        // ETAPA B: Buscar todos os alimentos de uma vez
        var allFoodIds = dishes
            .SelectMany(d => d.Ingredients)
            .Select(i => i.FoodId)
            .Distinct()
            .ToArray();

        var allFoods = await _foodRepository.GetManyByIdsAsync(allFoodIds);
        Console.WriteLine(allFoods);
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // ETAPA C: Montar os DTOs
        return dishes.Select(dish =>
        {
            // Mantém a ordem original dos ingredientes do prato
            var foodsForThisDish = dish.Ingredients
                .Select(i => foodsMap.TryGetValue(i.FoodId, out var food) ? food : null)
                .Where(f => f != null)
                .ToList();

            return new DishDto(dish, foodsForThisDish);
        });
    }

}