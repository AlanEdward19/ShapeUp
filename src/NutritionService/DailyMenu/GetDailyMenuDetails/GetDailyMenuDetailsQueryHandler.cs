using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.GetDailyMenuDetails;

/// <summary>
/// Handles the retrieval of daily menu details by ID.
/// </summary>
/// <param name="repository"></param>
public class GetDailyMenuDetailsQueryHandler(IDailyMenuMongoRepository repository)
: IHandler<DailyMenuDto, GetDailyMenuDetailsQuery>
{
    /// <summary>
    /// Handles the retrieval of daily menu details based on the provided query.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<DailyMenuDto> HandleAsync(GetDailyMenuDetailsQuery item, CancellationToken cancellationToken)
    {
        var dailyMenu = await repository.GetDailyMenuDetailsAsync(item.Id);
        if (dailyMenu == null)
            throw new NotFoundException(item.Id);

        return new DailyMenuDto(dailyMenu);
    }
}