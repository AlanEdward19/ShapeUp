using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;

namespace NutritionService.DailyMenu.CreateDailyMenu;

public class CreateDailyMenuCommandHandler(IDailyMenuMongoRepository repository) : 
    IHandler<DailyMenu, CreateDailyMenuCommand>
{
    public async Task<DailyMenu> HandleAsync(CreateDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var dailyMenu = item.ToDailyMenu();
        
        await repository.InsertDailyMenuAsync(dailyMenu);

        return dailyMenu;
    }
}