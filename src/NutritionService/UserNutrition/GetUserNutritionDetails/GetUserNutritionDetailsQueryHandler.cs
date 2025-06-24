using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.GetUserNutritionDetails;

/// <summary>
/// Handles the retrieval of user nutrition details by ID.
/// </summary>
/// <param name="repository"></param>
public class GetUserNutritionDetailsQueryHandler(IUserNutritionMongoRepository repository) : 
    IHandler<UserNutritionDto, GetUserNutritionDetailsQuery>
{
    /// <summary>
    /// Handles the retrieval of user nutrition details based on the provided query.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<UserNutritionDto> HandleAsync(GetUserNutritionDetailsQuery item, CancellationToken cancellationToken)
    {
        var userNutrition = await repository.GetUserNutritionDetailsAsync(item.Id);
        
        if (userNutrition == null)
            throw new NotFoundException(item.Id);
        
        return new UserNutritionDto(userNutrition);
    }
}