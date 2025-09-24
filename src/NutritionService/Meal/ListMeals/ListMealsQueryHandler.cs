using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository; // Repositório de Pratos
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository; // Repositório de Alimentos

namespace NutritionService.Meal.ListMeals;

/// <summary>
/// ListMealsQueryHandler handles the query to list meals.
/// </summary>
public class ListMealsQueryHandler : IHandler<IEnumerable<MealDto>, ListMealsQuery>
{
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public ListMealsQueryHandler(
        IMealMongoRepository mealRepository, 
        IDishMongoRepository dishRepository, 
        IUserFoodMongoRepository foodRepository)
    {
        _mealRepository = mealRepository;
        _dishRepository = dishRepository;
        _foodRepository = foodRepository;
    }

    /// <summary>
    /// Handles the retrieval of meals based on pagination parameters.
    /// </summary>
    public async Task<IEnumerable<MealDto>> HandleAsync(ListMealsQuery item, CancellationToken cancellationToken)
    {
        // 1. Buscar a lista paginada de Refeições (Meals)
        var meals = (await _mealRepository.ListMealsAsync(item.Page, item.Rows, cancellationToken, item.UserId)).ToList();

        if (!meals.Any())
        {
            return Enumerable.Empty<MealDto>();
        }

        // 2. Coletar todos os IDs necessários (Pratos e Alimentos) de todas as refeições
        var allDishIds = meals.SelectMany(m => m.DishIds).Distinct().ToArray();
        var allMealFoodIds = meals.SelectMany(m => m.Ingredients).Select(i => i.FoodId).ToList();

        // 3. Buscar todos os Pratos (Dishes) necessários de uma só vez
        var allDishes = (await _dishRepository.GetManyByIdsAsync(allDishIds, cancellationToken)).ToList();

        // 4. Coletar os IDs de Alimentos que compõem esses pratos
        var allDishFoodIds = allDishes.SelectMany(d => d.Ingredients).Select(i => i.FoodId).ToList();

        // 5. Unir todos os IDs de alimentos (dos pratos e das refeições) e buscar de uma só vez
        var finalFoodIds = allMealFoodIds.Union(allDishFoodIds).Distinct().ToArray();
        var allFoods = (await _foodRepository.GetManyByIdsAsync(finalFoodIds)).ToList();
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // 6. Montar os DishDtos completos
        var dishDtosMap = allDishes.ToDictionary(
            d => d.Id,
            d => new DishDto(d, d.Ingredients
                .Where(i => foodsMap.ContainsKey(i.FoodId))
                .Select(i => foodsMap[i.FoodId])
                .ToList())
        );

        // 7. Montar a lista final de MealDtos
        var mealsDto = meals.Select(meal =>
        {
            var dishesForThisMeal = meal.DishIds
                .Where(id => dishDtosMap.ContainsKey(id))
                .Select(id => dishDtosMap[id])
                .ToList();
                
            var foodsForThisMeal = meal.Ingredients
                .Where(i => foodsMap.ContainsKey(i.FoodId))
                .Select(i => foodsMap[i.FoodId])
                .ToList();

            return new MealDto(meal, dishesForThisMeal, foodsForThisMeal);
        }).ToList();

        return mealsDto;
    }
}