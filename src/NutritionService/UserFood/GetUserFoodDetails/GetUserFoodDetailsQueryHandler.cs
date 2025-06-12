using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.GetUserFoodDetails;

public class GetUserFoodDetailsQueryHandler(IUserFoodMongoRepository repository) : IHandler<Food, GetUserFoodDetailsQuery>
{
    public async Task<Food> HandleAsync(GetUserFoodDetailsQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetUserFoodByIdAsync(query.Id);
        
        if (food == null)
            throw new NotFoundException($"Food with id '{query.Id}' not found");

        return food;
    }
}