using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;

namespace NutritionService.UserNutrition.ListUserNutritions;

public class ListUserNutritionsQueryHandler(IUserNutritionMongoRepository repository) : IHandler<IEnumerable<UserNutrition>, ListUserNutritionsQuery>
{
    public async Task<IEnumerable<UserNutrition>> HandleAsync(ListUserNutritionsQuery item, CancellationToken cancellationToken)
    {
        return await repository.ListUserNutritionsAsync(item.Page, item.Rows, cancellationToken);
    }
}