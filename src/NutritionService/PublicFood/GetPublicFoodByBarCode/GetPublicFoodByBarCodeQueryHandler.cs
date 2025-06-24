using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.PublicFood.GetPublicFoodDetails;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.GetPublicFoodByBarCode;

/// <summary>
/// Handles the query to get public food details by bar code.
/// </summary>
/// <param name="repository"></param>
public class GetPublicFoodByBarCodeQueryHandler(IPublicFoodMongoRepository repository) : IHandler<FoodDto, GetPublicFoodByBarCodeQuery>
{
    /// <summary>
    /// Handles the query to retrieve public food details by its bar code.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<FoodDto> HandleAsync(GetPublicFoodByBarCodeQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetPublicFoodByBarCodeAsync(query.BarCode);
        
        if (food == null)
            throw new NotFoundException($"Food with barCode '{query.BarCode}' not found");

        return new FoodDto(food);
    }
}