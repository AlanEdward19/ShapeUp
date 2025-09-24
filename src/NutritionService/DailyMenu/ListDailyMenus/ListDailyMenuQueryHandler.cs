using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionService.DailyMenu.ListDailyMenus;

/// <summary>
/// Handles the query to list daily menus with full details.
/// </summary>
public class ListDailyMenuQueryHandler : IHandler<IEnumerable<DailyMenuDto>, ListDailyMenuQuery>
{
    private readonly IDailyMenuMongoRepository _dailyMenuRepository;
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public ListDailyMenuQueryHandler(
        IDailyMenuMongoRepository dailyMenuRepository,
        IMealMongoRepository mealRepository,
        IDishMongoRepository dishRepository,
        IUserFoodMongoRepository foodRepository)
    {
        _dailyMenuRepository = dailyMenuRepository;
        _mealRepository = mealRepository;
        _dishRepository = dishRepository;
        _foodRepository = foodRepository;
    }

    /// <summary>
    /// Handles the retrieval of daily menus based on the provided query parameters.
    /// </summary>
    public async Task<IEnumerable<DailyMenuDto>> HandleAsync(ListDailyMenuQuery item, CancellationToken cancellationToken)
    {
        // 1. Busca a lista de DailyMenus com base nos filtros
        var dailyMenus = (await GetDailyMenusFromRepository(item, cancellationToken)).ToList();

        if (!dailyMenus.Any())
        {
            return Enumerable.Empty<DailyMenuDto>();
        }

        // --- ORQUESTRAÇÃO PARA BUSCAR TODOS OS DADOS NECESSÁRIOS ---

        // 2. Coleta todos os IDs de Refeições (Meals)
        var allMealIds = dailyMenus.SelectMany(dm => dm.MealIds).Distinct().ToArray();
        var allMeals = (await _mealRepository.GetManyMealsByIdsAsync(allMealIds, cancellationToken)).ToList();
        var mealsMap = allMeals.ToDictionary(m => m.Id);

        // 3. Coleta todos os IDs de Pratos (Dishes) e Alimentos (Foods) das refeições
        var allDishIds = allMeals.SelectMany(m => m.DishIds).Distinct().ToArray();
        var allMealFoodIds = allMeals.SelectMany(m => m.Ingredients).Select(i => i.FoodId).ToList();

        // 4. Busca todos os Pratos
        var allDishes = (await _dishRepository.GetManyByIdsAsync(allDishIds, cancellationToken)).ToList();
        
        // 5. Coleta os IDs de Alimentos dos pratos e busca TODOS os alimentos de uma vez
        var allDishFoodIds = allDishes.SelectMany(d => d.Ingredients).Select(i => i.FoodId).ToList();
        var finalFoodIds = allMealFoodIds.Union(allDishFoodIds).Distinct().ToArray();
        var allFoods = (await _foodRepository.GetManyByIdsAsync(finalFoodIds)).ToList();
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // 6. Monta os DishDtos
        var dishDtosMap = allDishes.ToDictionary(
            d => d.Id,
            d => new DishDto(d, d.Ingredients
                .Where(i => foodsMap.ContainsKey(i.FoodId))
                .Select(i => foodsMap[i.FoodId])
                .ToList())
        );
        
        // 7. Monta os MealDtos
        var mealDtosMap = allMeals.ToDictionary(
            m => m.Id,
            m =>
            {
                var dishesForMeal = m.DishIds.Where(id => dishDtosMap.ContainsKey(id)).Select(id => dishDtosMap[id]).ToList();
                var foodsForMeal = m.Ingredients.Where(i => foodsMap.ContainsKey(i.FoodId)).Select(i => foodsMap[i.FoodId]).ToList();
                return new MealDto(m, dishesForMeal, foodsForMeal);
            }
        );

        // 8. Monta a lista final de DailyMenuDto
        var dailyMenuDtos = dailyMenus.Select(dm => 
            new DailyMenuDto(dm, dm.MealIds
                .Where(id => mealDtosMap.ContainsKey(id))
                .Select(id => mealDtosMap[id])
                .ToList())
        ).ToList();
        
        return dailyMenuDtos;
    }

    /// <summary>
    /// Método auxiliar para encapsular a lógica de filtro e chamada ao repositório.
    /// </summary>
    private Task<IEnumerable<DailyMenu>> GetDailyMenusFromRepository(ListDailyMenuQuery item, CancellationToken cancellationToken)
    {
        var day = item.DayOfWeek?.Trim();
        
        return day switch
        {
            // Lista todos os cardápios
            null => _dailyMenuRepository.ListDailyMenusAsync(item.Page, item.Size, item.UserId),
            // Lista os cardápios que o atributo dayofweek é nulo
            "" => _dailyMenuRepository.ListDailyMenusAsync(null, item.Page, item.Size, item.UserId),
            // Filtra por dia da semana
            _ => Enum.TryParse<DayOfWeek>(day, true, out var parsedDay)
                ? _dailyMenuRepository.ListDailyMenusAsync(parsedDay, item.Page, item.Size, item.UserId)
                : throw new ArgumentException($"Invalid day of the week: {item.DayOfWeek}")
        };
    }
}