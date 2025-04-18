using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;

namespace NutritionService.DailyMenu.ListDailyMenus;

public class ListDailyMenuQueryHandler(IDailyMenuMongoRepository repository) : IHandler<IEnumerable<DailyMenu>, ListDailyMenuQuery>
{
    public async Task<IEnumerable<DailyMenu>> HandleAsync(ListDailyMenuQuery item, CancellationToken cancellationToken)
    {
        switch (item.DayOfWeek)
        {
            case null:
                await repository.ListDailyMenusAsync(item.Page, item.Size);
                break;
            case "":
                await repository.ListDailyMenusAsync(null, item.Page, item.Size);
                break;
        }

        if (Enum.TryParse<DayOfWeek>(item.DayOfWeek, true, out var parsedDay))
        {
            await repository.ListDailyMenusAsync(parsedDay, item.Page, item.Size);
        }
        throw new ArgumentException($"Invalid day of the week: {item.DayOfWeek}");
    }
}