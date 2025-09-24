using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Dish.CreateDishForSameUser;

/// <summary>
/// Handles the creation of a new dish.
/// </summary>
public class CreateDishForSameUserCommandHandler : IHandler<DishDto, CreateDishForSameUserCommand>
{
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _userFoodRepository;

    public CreateDishForSameUserCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository userFoodRepository)
    {
        _dishRepository = dishRepository;
        _userFoodRepository = userFoodRepository;
    }

    /// <summary>
    /// Handles the creation of a new dish based on the provided command.
    /// </summary>
    public async Task<DishDto> HandleAsync(CreateDishForSameUserCommand item, CancellationToken cancellationToken)
    {
        // 1. Mapeia o input do comando para a lista de entidades de domínio Ingredient.
        var ingredients = item.Ingredients
            .Select(i => new Ingredient(i.FoodId, i.Quantity))
            .ToList();
        
        // 2. Cria a nova entidade Dish com a lista de ingredientes.
        var dish = new Dish(item.Name, ingredients);
        dish.SetId();
        dish.SetCreatedBy(ProfileContext.ProfileId);
        dish.SetUserId(ProfileContext.ProfileId);
        
        await _dishRepository.InsertDishAsync(dish);

        // 3. Busca os detalhes dos alimentos para montar o DTO de resposta.
        var foodIds = dish.Ingredients.Select(i => i.FoodId).ToArray();
        var foodsForDto = await _userFoodRepository.GetManyByIdsAsync(foodIds);

        // 4. Retorna o DTO completo.
        return new DishDto(dish, foodsForDto.ToList());
    }
}