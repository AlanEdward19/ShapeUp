using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository; // 1. Adicionar o using para o repositório de alimentos
using SharedKernel.Exceptions;

namespace NutritionService.Dish.GetDishDetails;

/// <summary>
/// Handles the retrieval of dish details by ID.
/// </summary>
public class GetDishDetailsQueryHandler : IHandler<DishDto, GetDishDetailsQuery>
{
    private readonly IDishMongoRepository _dishRepository;
    private readonly IUserFoodMongoRepository _foodRepository; // 2. Declarar o novo repositório

    /// <summary>
    /// Construtor com injeção de dependência para os repositórios de pratos e alimentos.
    /// </summary>
    public GetDishDetailsQueryHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository foodRepository) // 3. Injetar o repositório no construtor
    {
        _dishRepository = dishRepository;
        _foodRepository = foodRepository;
    }

    /// <summary>
    /// Handles the retrieval of dish details based on the provided query.
    /// </summary>
    public async Task<DishDto> HandleAsync(GetDishDetailsQuery item, CancellationToken cancellationToken)
    {
        // ETAPA A: Buscar o prato principal
        var dish = await _dishRepository.GetDishByIdAsync(item.Id);

        if (dish == null || dish.Ingredients == null || !dish.Ingredients.Any())
        {
            throw new NotFoundException($"Dish with ID {item.Id} not found or has no ingredients.");
        }

        // ETAPA B: Buscar os detalhes dos alimentos (Foods)
        // Pega todos os IDs únicos dos ingredientes para uma consulta eficiente
        var foodIds = dish.Ingredients.Select(i => i.FoodId).Distinct().ToArray();
        
        // Busca todos os alimentos correspondentes de uma só vez
        var foods = await _foodRepository.GetManyByIdsAsync(foodIds);

        if (foods == null)
        {
            // Opcional: você pode decidir se isso deve ser um erro ou apenas retornar um prato com ingredientes vazios.
            // Lançar uma exceção é mais seguro para garantir a consistência dos dados.
            throw new NotFoundException($"Could not find the food items for Dish with ID {item.Id}.");
        }
        
        // ETAPA C: Montar o DTO final com todas as informações
        return new DishDto(dish, foods.ToList()); // 4. Usar o novo construtor do DishDto
    }
}