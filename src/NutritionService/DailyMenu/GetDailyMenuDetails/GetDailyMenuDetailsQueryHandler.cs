using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionService.DailyMenu.GetDailyMenuDetails;

/// <summary>
/// Handles the retrieval of daily menu details by ID, orchestrating the fetch of all related data.
/// </summary>
public class GetDailyMenuDetailsQueryHandler : IHandler<DailyMenuDto, GetDailyMenuDetailsQuery>
{
    private readonly IDailyMenuMongoRepository _dailyMenuRepository;
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public GetDailyMenuDetailsQueryHandler(
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
    /// Handles the retrieval and full composition of daily menu details.
    /// </summary>
    public async Task<DailyMenuDto> HandleAsync(GetDailyMenuDetailsQuery item, CancellationToken cancellationToken)
    {
        // 1. Buscar o DailyMenu principal
        var dailyMenu = await _dailyMenuRepository.GetDailyMenuDetailsAsync(item.Id);
        if (dailyMenu == null)
            throw new NotFoundException($"DailyMenu with id '{item.Id}' not found");

        // 2. Buscar todas as Refeições (Meals) do menu
        var meals = (await _mealRepository.GetManyMealsByIdsAsync(dailyMenu.MealIds.ToArray(), cancellationToken)).ToList();
        
        // 3. Coletar todos os IDs de Pratos e Alimentos das refeições
        var allDishIds = meals.SelectMany(m => m.DishIds).Distinct().ToArray();
        var allMealFoodIds = meals.SelectMany(m => m.Ingredients).Select(i => i.FoodId).ToList();

        // 4. Buscar todos os Pratos
        var allDishes = (await _dishRepository.GetManyByIdsAsync(allDishIds, cancellationToken)).ToList();

        // 5. Coletar IDs de Alimentos dos pratos e buscar todos os alimentos de uma vez
        var allDishFoodIds = allDishes.SelectMany(d => d.Ingredients).Select(i => i.FoodId).ToList();
        var finalFoodIds = allMealFoodIds.Union(allDishFoodIds).Distinct().ToArray();
        var allFoods = (await _foodRepository.GetManyByIdsAsync(finalFoodIds)).ToList();
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // 6. Montar os DishDtos
        var dishDtosMap = allDishes.ToDictionary(
            d => d.Id,
            d => new DishDto(d, d.Ingredients
                .Where(i => foodsMap.ContainsKey(i.FoodId))
                .Select(i => foodsMap[i.FoodId])
                .ToList())
        );
        
        // 7. Montar os MealDtos
        var mealDtos = meals.Select(m =>
        {
            var dishesForMeal = m.DishIds.Where(id => dishDtosMap.ContainsKey(id)).Select(id => dishDtosMap[id]).ToList();
            var foodsForMeal = m.Ingredients.Where(i => foodsMap.ContainsKey(i.FoodId)).Select(i => foodsMap[i.FoodId]).ToList();
            return new MealDto(m, dishesForMeal, foodsForMeal);
        }).ToList();

        // 8. Montar o DailyMenuDto final
        return new DailyMenuDto(dailyMenu, mealDtos);
    }
}