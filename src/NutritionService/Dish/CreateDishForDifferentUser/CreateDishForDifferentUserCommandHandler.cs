using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Dish.CreateDishForDifferentUser;

public class CreateDishForDifferentUserCommandHandler : IHandler<DishDto, CreateDishForDifferentUserCommand>
{
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _userFoodRepository;

    public CreateDishForDifferentUserCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository userFoodRepository)
    {
        _dishRepository = dishRepository;
        _userFoodRepository = userFoodRepository;
    }

    /// <summary>
    /// Handles the creation of a new dish for a different user.
    /// </summary>
    public async Task<DishDto> HandleAsync(CreateDishForDifferentUserCommand item, CancellationToken cancellationToken)
    {
        // 1. Mapeia o input do comando para a lista de entidades de domínio Ingredient.
        var ingredients = item.Ingredients
            .Select(i => new Ingredient(i.FoodId, i.Quantity))
            .ToList();
        
        // 2. Cria a nova entidade Dish.
        var dish = new Dish(item.Name, ingredients);
        dish.SetId();
        dish.SetCreatedBy(ProfileContext.ProfileId); // Criado pelo profissional logado
        dish.SetUserId(item.UserId);                 // Atribuído ao cliente/usuário alvo
        
        await _dishRepository.InsertDishAsync(dish);

        // 3. Busca os detalhes dos alimentos para montar o DTO de resposta.
        var foodIds = dish.Ingredients.Select(i => i.FoodId).ToArray();
        var foodsForDto = await _userFoodRepository.GetManyByIdsAsync(foodIds);

        // 4. Retorna o DTO completo.
        return new DishDto(dish, foodsForDto.ToList());
    }
}