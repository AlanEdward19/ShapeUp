using NutritionService.Common.Interfaces;
using NutritionService.Dish; // Usar DishDto
using NutritionService.Dish.Common.Repository; // Usar IDishMongoRepository
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository; // Usar IUserFoodMongoRepository
using SharedKernel.Exceptions;

namespace NutritionService.Meal.GetMealDetails;

/// <summary>
/// Handles the retrieval of meal details by ID.
/// </summary>
public class GetMealDetailsQueryHandler : IHandler<MealDto, GetMealDetailsQuery>
{
    private readonly IMealMongoRepository _mealRepository;
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository;

    public GetMealDetailsQueryHandler(
        IMealMongoRepository mealRepository, 
        IDishMongoRepository dishRepository, 
        IUserFoodMongoRepository foodRepository)
    {
        _mealRepository = mealRepository;
        _dishRepository = dishRepository;
        _foodRepository = foodRepository;
    }

    /// <summary>
    /// Handles the retrieval of meal details based on the provided query.
    /// </summary>
    public async Task<MealDto> HandleAsync(GetMealDetailsQuery item, CancellationToken cancellationToken)
    {
        // 1. Buscar a Refeição (Meal) principal
        var meal = await _mealRepository.GetMealByIdAsync(item.Id);
        if (meal == null)
            throw new NotFoundException($"Meal with id '{item.Id}' not found");

        // 2. Buscar os Pratos (Dishes) da refeição
        var dishes = (await _dishRepository.GetManyByIdsAsync(meal.DishIds.ToArray(), cancellationToken)).ToList();

        // 3. Coletar todos os IDs de Alimentos (Foods) necessários
        var mealFoodIds = meal.Ingredients.Select(i => i.FoodId).ToList();
        var dishFoodIds = dishes.SelectMany(d => d.Ingredients).Select(i => i.FoodId).ToList();
        var allFoodIds = mealFoodIds.Union(dishFoodIds).Distinct().ToArray();

        // 4. Buscar todos os Alimentos necessários de uma só vez
        var allFoods = (await _foodRepository.GetManyByIdsAsync(allFoodIds)).ToList();
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // 5. Montar os DishDtos completos
        var dishDtos = dishes.Select(d => 
            new DishDto(d, d.Ingredients
                .Where(i => foodsMap.ContainsKey(i.FoodId))
                .Select(i => foodsMap[i.FoodId])
                .ToList())
        ).ToList();

        // 6. Montar o MealDto final
        var foodsForMeal = meal.Ingredients
            .Where(i => foodsMap.ContainsKey(i.FoodId))
            .Select(i => foodsMap[i.FoodId])
            .ToList();

        return new MealDto(meal, dishDtos, foodsForMeal);
    }
}