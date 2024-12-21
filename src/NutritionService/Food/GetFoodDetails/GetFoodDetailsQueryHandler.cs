using NutritionService.Common.Interfaces;
using NutritionService.Exceptions;
using NutritionService.Food.Common.Repository;

namespace NutritionService.Food.GetFoodDetails;

public class GetFoodDetailsQueryHandler(IFoodMongoRepository repository) : IHandler<Food, GetFoodDetailsQuery>
{
    public async Task<Food> HandleAsync(GetFoodDetailsQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetFoodByBarCodeAsync(query.BarCode);
        
        if (food == null)
            throw new NotFoundException($"Food with barcode '{query.BarCode}' not found");

        return food;
    }
}