using NutritionService.Common.Interfaces;
using NutritionService.Dish;
using NutritionService.Dish.Common.Repository;
using NutritionService.Meal.Common;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Meal.CreateMealForSameUser;

/// <summary>
/// Handles the creation of a new meal.
/// </summary>
public class CreateMealForSameUserCommandHandler : IHandler<MealDto, CreateMealForSameUserCommand>
{
    private readonly IMealMongoRepository _mealRepository;
    private readonly IUserFoodMongoRepository _foodRepository;
    private readonly IDishMongoRepository _dishRepository;

    public CreateMealForSameUserCommandHandler(
        IMealMongoRepository mealRepository, 
        IUserFoodMongoRepository foodRepository, 
        IDishMongoRepository dishRepository)
    {
        _mealRepository = mealRepository;
        _foodRepository = foodRepository;
        _dishRepository = dishRepository;
    }

    /// <summary>
    /// Handles the creation of a new meal based on the provided command.
    /// </summary>
    public async Task<MealDto> HandleAsync(CreateMealForSameUserCommand item, CancellationToken cancellationToken)
    {
        // 1. Mapear inputs e criar a entidade Meal
        var ingredients = item.Ingredients.Select(i => new Ingredient(i.FoodId, i.Quantity)).ToList();
        var meal = new Meal(item.Type, item.Name, item.DishIds, ingredients);
        meal.SetId();
        meal.SetCreatedBy(ProfileContext.ProfileId);
        meal.SetUserId(ProfileContext.ProfileId);
        
        await _mealRepository.InsertMealAsync(meal);

        // --- Orquestração para montar o DTO de retorno ---

        // 2. Buscar os pratos (Dishes)
        var dishes = (await _dishRepository.GetManyByIdsAsync(meal.DishIds.ToArray(), cancellationToken)).ToList();

        // 3. Coletar todos os IDs de alimentos e buscá-los de uma vez
        var mealFoodIds = meal.Ingredients.Select(i => i.FoodId).ToList();
        var dishFoodIds = dishes.SelectMany(d => d.Ingredients).Select(i => i.FoodId).ToList();
        var allFoodIds = mealFoodIds.Union(dishFoodIds).Distinct().ToArray();
        var allFoods = (await _foodRepository.GetManyByIdsAsync(allFoodIds)).ToList();
        var foodsMap = allFoods.ToDictionary(f => f.Id);

        // 4. Montar os DishDtos
        var dishDtos = dishes.Select(d => 
            new DishDto(d, d.Ingredients
                .Where(i => foodsMap.ContainsKey(i.FoodId))
                .Select(i => foodsMap[i.FoodId])
                .ToList())
        ).ToList();

        // 5. Montar o MealDto final
        var foodsForMeal = meal.Ingredients
            .Where(i => foodsMap.ContainsKey(i.FoodId))
            .Select(i => foodsMap[i.FoodId])
            .ToList();

        return new MealDto(meal, dishDtos, foodsForMeal);
    }
}