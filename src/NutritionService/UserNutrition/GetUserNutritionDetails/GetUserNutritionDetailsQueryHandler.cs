using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.GetUserNutritionDetails;

public class GetUserNutritionDetailsQueryHandler(IUserNutritionMongoRepository repository) : 
    IHandler<UserNutrition, GetUserNutritionDetailsQuery>
{
    public async Task<UserNutrition> HandleAsync(GetUserNutritionDetailsQuery item, CancellationToken cancellationToken)
    {
        var userNutrition = await repository.GetUserNutritionDetailsAsync(item.Id);
        
        if (userNutrition == null)
            throw new NotFoundException(item.Id);
        
        return userNutrition;
    }
}