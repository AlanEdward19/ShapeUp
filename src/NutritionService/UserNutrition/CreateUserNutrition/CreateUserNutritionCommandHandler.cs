using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu;
using NutritionService.DailyMenu.Common;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutritionService.UserNutrition.CreateUserNutrition;

/// <summary>
/// Handles the creation of a user's nutrition plan.
/// </summary>
public class CreateUserNutritionCommandHandler : IHandler<UserNutritionDto, CreateUserNutritionCommand>
{
    private readonly IUserNutritionMongoRepository _userNutritionRepository;
    private readonly IDailyMenuMongoRepository _dailyMenuRepository;
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public CreateUserNutritionCommandHandler(
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
    /// Handles the creation and full data retrieval for a new user nutrition plan.
    /// </summary>
    public async Task<UserNutritionDto> HandleAsync(CreateUserNutritionCommand item, CancellationToken cancellationToken)
    {
        // 1. Criar a entidade UserNutrition com as referências
        var userNutrition = new UserNutrition(item.NutritionManagerId, item.DailyMenuIds);
        userNutrition.SetId();
        userNutrition.SetCreatedBy(ProfileContext.ProfileId);
        userNutrition.SetUserId(item.UserId);
        
        await _userNutritionRepository.InsertUserNutritionAsync(userNutrition);

        // --- Orquestração Mestra para montar o DTO de retorno ---

        // 2. Buscar DailyMenus
        var dailyMenus = (await _dailyMenuRepository.GetManyByIdsAsync(userNutrition.DailyMenuIds.ToArray(), cancellationToken)).ToList();
        
        // 3. Buscar Meals
        var allMealIds = dailyMenus.SelectMany(dm => dm.MealIds).Distinct().ToArray();
        var allMeals = (await _mealRepository.GetManyMealsByIdsAsync(allMealIds, cancellationToken)).ToList();
        var mealsMap = allMeals.ToDictionary(m => m.Id);

        // 4. Buscar Dishes e coletar FoodIds
        var allDishIds = allMeals.SelectMany(m => m.DishIds).Distinct().ToArray();
        var allMealFoodIds = allMeals.SelectMany(m => m.Ingredients).Select(i => i.FoodId).ToList();
        var allDishes = (await _dishRepository.GetManyByIdsAsync(allDishIds, cancellationToken)).ToList();

        // 5. Buscar todos os Foods necessários de uma só vez
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
        var dailyMenuDtos = dailyMenus.Select(dm => 
            new DailyMenuDto(dm, dm.MealIds.Where(id => mealDtosMap.ContainsKey(id)).Select(id => mealDtosMap[id]).ToList())
        ).ToList();

        // 9. Montar e retornar o UserNutritionDto final
        return new UserNutritionDto(userNutrition, dailyMenuDtos);
    }
}