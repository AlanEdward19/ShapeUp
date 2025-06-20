using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.PublicFood.GetPublicFoodDetails;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.GetPublicFoodByBarCode;

public class GetPublicFoodByBarCodeQueryHandler(IPublicFoodMongoRepository repository) : IHandler<Food, GetPublicFoodByBarCodeQuery>
{
    public async Task<Food> HandleAsync(GetPublicFoodByBarCodeQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetPublicFoodByBarCodeAsync(query.BarCode);
        
        if (food == null)
            throw new NotFoundException($"Food with barCode '{query.BarCode}' not found");

        return food;
    }
}