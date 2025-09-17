using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;

namespace NutritionService.UserNutrition.ListManagedUserNutritions;

/// <summary>
/// ListUserNutritionsQueryHandler handles the query to list user nutrition records.
/// </summary>
/// <param name="repository"></param>
public class ListManagedUserNutritionsQueryHandler(IUserNutritionMongoRepository repository) : IHandler<IEnumerable<UserNutritionDto>, ListManagedUserNutritionsQuery>
{
    /// <summary>
    /// Handles the retrieval of user nutrition records based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UserNutritionDto>> HandleAsync(ListManagedUserNutritionsQuery item, CancellationToken cancellationToken)
    {
        var userNutrition = await repository.ListManagedUserNutritionsAsync(item.Page, item.Rows, cancellationToken, item.NutritionManagerId);
        var userNutritionDto = userNutrition.Select(u => new UserNutritionDto(u));
        return  userNutritionDto;
    }
}