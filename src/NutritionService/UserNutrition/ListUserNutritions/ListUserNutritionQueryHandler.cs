using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;

namespace NutritionService.UserNutrition.ListUserNutritions;

/// <summary>
/// ListUserNutritionsQueryHandler handles the query to list user nutrition records.
/// </summary>
/// <param name="repository"></param>
public class ListUserNutritionQueryHandler(IUserNutritionMongoRepository repository) : IHandler<IEnumerable<UserNutritionDto>, ListUserNutritionsQuery>
{
    /// <summary>
    /// Handles the retrieval of user nutrition records based on pagination parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UserNutritionDto>> HandleAsync(ListUserNutritionsQuery item, CancellationToken cancellationToken)
    {
        var userNutrition = await repository.ListUserNutritionsAsync(item.Page, item.Rows, cancellationToken);
        var userNutritionDto = userNutrition.Select(u => new UserNutritionDto(u));
        return  userNutritionDto;
    }
}