using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutritionService.DailyMenu.CreateDailyMenuForSameUser;

/// <summary>
/// Handles the creation of a daily menu for the same user.
/// </summary>
public class CreateDailyMenuForSameUserCommandHandler : IHandler<DailyMenuDto, CreateDailyMenuForSameUserCommand>
{
    private readonly IDailyMenuMongoRepository _dailyMenuRepository;
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public CreateDailyMenuForSameUserCommandHandler(
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
    /// Handles the creation and full data retrieval for a new daily menu.
    /// </summary>
    public async Task<DailyMenuDto> HandleAsync(CreateDailyMenuForSameUserCommand item, CancellationToken cancellationToken)
    {
        // 1. Criar a entidade DailyMenu com as referências (MealIds)
        var dailyMenu = new DailyMenu(item.DayOfWeek, item.MealIds);
        dailyMenu.SetId();
        dailyMenu.SetCreatedBy(ProfileContext.ProfileId);
        dailyMenu.SetUserId(ProfileContext.ProfileId);
        
        await _dailyMenuRepository.InsertDailyMenuAsync(dailyMenu);

        // --- Orquestração para montar o DTO de retorno ---

        // 2. Buscar todas as Refeições (Meals)
        var meals = (await _mealRepository.GetManyMealsByIdsAsync(dailyMenu.MealIds.ToArray(), cancellationToken)).ToList();

        // 3. Coletar IDs de Pratos e Alimentos
        var allDishIds = meals.SelectMany(m => m.DishIds).Distinct().ToArray();
        var allMealFoodIds = meals.SelectMany(m => m.Ingredients).Select(i => i.FoodId).ToList();

        // 4. Buscar todos os Pratos
        var allDishes = (await _dishRepository.GetManyByIdsAsync(allDishIds, cancellationToken)).ToList();

        // 5. Coletar IDs de Alimentos dos pratos e buscar TODOS os alimentos
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

        // 8. Montar e retornar o DailyMenuDto final
        return new DailyMenuDto(dailyMenu, mealDtos);
    }
}