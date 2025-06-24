using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.GetPublicFoodDetails;

/// <summary>
/// Handles the retrieval of public food details by ID.
/// </summary>
/// <param name="repository"></param>
public class GetPublicFoodDetailsQueryHandler(IPublicFoodMongoRepository repository) : IHandler<FoodDto, GetPublicFoodDetailsQuery>
{
    /// <summary>
    /// Handles the retrieval of public food details based on the provided query.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<FoodDto> HandleAsync(GetPublicFoodDetailsQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetPublicFoodByIdAsync(query.Id);
        
        if (food == null)
            throw new NotFoundException($"Food with id '{query.Id}' not found");

        return new FoodDto(food);
    }
}