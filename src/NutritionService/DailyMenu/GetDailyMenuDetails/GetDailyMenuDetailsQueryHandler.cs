using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.GetDailyMenuDetails;

public class GetDailyMenuDetailsQueryHandler(IDailyMenuMongoRepository repository)
: IHandler<DailyMenu, GetDailyMenuDetailsQuery>
{
    public async Task<DailyMenu> HandleAsync(GetDailyMenuDetailsQuery item, CancellationToken cancellationToken)
    {
        var dailyMenu = await repository.GetDailyMenuDetailsAsync(item.Id);
        if (dailyMenu == null)
            throw new NotFoundException(item.Id);

        return dailyMenu;
    }
}