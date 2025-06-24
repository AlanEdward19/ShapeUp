using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.GetUserFoodByBarCode;

public class GetUserFoodByBarCodeQueryHandler(IUserFoodMongoRepository repository) : IHandler<FoodDto, GetUserFoodByBarCodeQuery>
{
    public async Task<FoodDto> HandleAsync(GetUserFoodByBarCodeQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetUserFoodByBarCodeAsync(query.BarCode);
        
        if (food == null)
            throw new NotFoundException($"Food with barCode '{query.BarCode}' not found");

        return new FoodDto(food);
    }
}