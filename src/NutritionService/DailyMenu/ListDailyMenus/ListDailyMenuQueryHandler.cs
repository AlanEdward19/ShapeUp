using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;

namespace NutritionService.DailyMenu.ListDailyMenus;

/// <summary>
/// ListDailyMenuQueryHandler handles the query to list daily menus.
/// </summary>
/// <param name="repository"></param>
public class ListDailyMenuQueryHandler(IDailyMenuMongoRepository repository) : IHandler<IEnumerable<DailyMenuDto>, ListDailyMenuQuery>
{
    /// <summary>
    /// Handles the retrieval of daily menus based on the provided query parameters.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<IEnumerable<DailyMenuDto>> HandleAsync(ListDailyMenuQuery item, CancellationToken cancellationToken)
    {
        var day = item.DayOfWeek?.Trim();
        IEnumerable<DailyMenu> dailyMenus;
        IEnumerable<DailyMenuDto> dailyMenusDto;
        switch (day)
        {
            case null:
                dailyMenus = await repository.ListDailyMenusAsync(item.Page, item.Size);
                dailyMenusDto = dailyMenus.Select(menu => new DailyMenuDto(menu));
                return dailyMenusDto;
            case "":
                dailyMenus = await repository.ListDailyMenusAsync(null, item.Page, item.Size);
                dailyMenusDto = dailyMenus.Select(menu => new DailyMenuDto(menu));
                return dailyMenusDto;
        }

        if (Enum.TryParse<DayOfWeek>(item.DayOfWeek, true, out var parsedDay))
        {
            dailyMenus = await repository.ListDailyMenusAsync(parsedDay, item.Page, item.Size);
            dailyMenusDto = dailyMenus.Select(menu => new DailyMenuDto(menu));
            return dailyMenusDto;
        }
        throw new ArgumentException($"Invalid day of the week: {item.DayOfWeek}");
    }
}