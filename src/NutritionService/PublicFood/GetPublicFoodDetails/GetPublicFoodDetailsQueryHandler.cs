using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood;
using SharedKernel.Exceptions;

namespace NutritionService.PublicFood.GetPublicFoodDetails;

public class GetPublicFoodDetailsQueryHandler(IPublicFoodMongoRepository repository) : IHandler<Food, GetPublicFoodDetailsQuery>
{
    public async Task<Food> HandleAsync(GetPublicFoodDetailsQuery query, CancellationToken cancellationToken)
    {
        var food = await repository.GetPublicFoodByIdAsync(query.Id);
        
        if (food == null)
            throw new NotFoundException($"Food with id '{query.Id}' not found");

        return food;
    }
}