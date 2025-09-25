using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu;
using NutritionService.DailyMenu.Common;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using NutritionService.UserNutrition.Common.Repository;

namespace NutritionService.UserNutrition.ListUserNutritions;

/// <summary>
/// Handles the query to list all nutrition records for a specific user.
/// </summary>
public class ListUserNutritionsQueryHandler : IHandler<IEnumerable<UserNutritionDto>, ListUserNutritionsQuery>
{
    private readonly IUserNutritionMongoRepository _userNutritionRepository;
    private readonly IDailyMenuMongoRepository _dailyMenuRepository;
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public ListUserNutritionsQueryHandler(
        IUserNutritionMongoRepository userNutritionRepository,
        IDailyMenuMongoRepository dailyMenuRepository,
        IMealMongoRepository mealRepository,
        IDishMongoRepository dishRepository,
        IUserFoodMongoRepository foodRepository)
    {
        _userNutritionRepository = userNutritionRepository;
        _dailyMenuRepository = dailyMenuRepository;
        _mealRepository = mealRepository;
        _dishRepository = dishRepository;
        _foodRepository = foodRepository;
    }

    /// <summary>
    /// Handles the retrieval and full composition of a specific user's nutrition plans.
    /// </summary>
    public async Task<IEnumerable<UserNutritionDto>> HandleAsync(ListUserNutritionsQuery item, CancellationToken cancellationToken)
    {
        // 1. Buscar a lista de UserNutrition pelo UserId
        var userNutritions = (await _userNutritionRepository.ListUserNutritionsAsync(item.UserId, cancellationToken)).ToList();
        if (!userNutritions.Any())
        {
            return Enumerable.Empty<UserNutritionDto>();
        }

        // 2. Buscar DailyMenus
        var allDailyMenuIds = userNutritions.SelectMany(un => un.DailyMenuIds).Distinct().ToArray();
        var allDailyMenus = (await _dailyMenuRepository.GetManyByIdsAsync(allDailyMenuIds, cancellationToken)).ToList();
        var dailyMenusMap = allDailyMenus.ToDictionary(dm => dm.Id);

        // 3. Buscar Meals
        var allMealIds = allDailyMenus.SelectMany(dm => dm.MealIds).Distinct().ToArray();
        var allMeals = (await _mealRepository.GetManyMealsByIdsAsync(allMealIds, cancellationToken)).ToList();
        var mealsMap = allMeals.ToDictionary(m => m.Id);

        // 4. Buscar Dishes e coletar FoodIds
        var allDishIds = allMeals.SelectMany(m => m.DishIds).Distinct().ToArray();
        var allMealFoodIds = allMeals.SelectMany(m => m.Ingredients).Select(i => i.FoodId).ToList();
        var allDishes = (await _dishRepository.GetManyByIdsAsync(allDishIds, cancellationToken)).ToList();

        // 5. Buscar todos os Foods de uma só vez
        var allDishFoodIds = allDishes.SelectMany(d => d.Ingredients).Select(i => i.FoodId).ToList();
        var finalFoodIds = allMealFoodIds.Union(allDishFoodIds).Distinct().ToArray();
        var allFoods = (await _foodRepository.GetManyByIdsAsync(finalFoodIds)).ToList();
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // 6. Montar DishDtos
        var dishDtosMap = allDishes.ToDictionary(
            d => d.Id,
            d => new DishDto(d, d.Ingredients.Where(i => foodsMap.ContainsKey(i.FoodId)).Select(i => foodsMap[i.FoodId]).ToList())
        );
        
        // 7. Montar MealDtos
        var mealDtosMap = allMeals.ToDictionary(
            m => m.Id,
            m => {
                var dishesForMeal = m.DishIds.Where(id => dishDtosMap.ContainsKey(id)).Select(id => dishDtosMap[id]).ToList();
                var foodsForMeal = m.Ingredients.Where(i => foodsMap.ContainsKey(i.FoodId)).Select(i => foodsMap[i.FoodId]).ToList();
                return new MealDto(m, dishesForMeal, foodsForMeal);
            }
        );

        // 8. Montar DailyMenuDtos
        var dailyMenuDtosMap = allDailyMenus.ToDictionary(
            dm => dm.Id,
            dm => new DailyMenuDto(dm, dm.MealIds.Where(id => mealDtosMap.ContainsKey(id)).Select(id => mealDtosMap[id]).ToList())
        );

        // 9. Montar a lista final de UserNutritionDto
        var userNutritionDtos = userNutritions.Select(un => 
            new UserNutritionDto(un, un.DailyMenuIds.Where(id => dailyMenuDtosMap.ContainsKey(id)).Select(id => dailyMenuDtosMap[id]).ToList())
        ).ToList();

        return userNutritionDtos;
    }
}