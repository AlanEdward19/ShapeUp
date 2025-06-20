using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.GetUserFoodByBarCode;

public class GetUserFoodByBarCodeQueryHandler(IUserFoodMongoRepository repository) : IHandler<Food, GetUserFoodByBarCodeQuery>
{
    public async Task<Food> HandleAsync(GetUserFoodByBarCodeQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetUserFoodByBarCodeAsync(query.BarCode);
        
        if (food == null)
            throw new NotFoundException($"Food with barCode '{query.BarCode}' not found");

        return food;
    }
}