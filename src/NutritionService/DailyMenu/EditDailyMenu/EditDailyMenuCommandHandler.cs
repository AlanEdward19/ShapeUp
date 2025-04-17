using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.EditDailyMenu;

public class EditDailyMenuCommandHandler(IDailyMenuMongoRepository repository):
    IHandler<DailyMenu, EditDailyMenuCommand>
{
    public async Task<DailyMenu> HandleAsync(EditDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var existingDailyMenu = await repository.GetDailyMenuDetailsAsync(item.Id);
        
        if (existingDailyMenu == null)
            throw new NotFoundException(item.Id);
        
        existingDailyMenu.UpdateInfo(existingDailyMenu.DayOfWeek, existingDailyMenu.Meals);
        
        await repository.UpdateDailyMenuAsync(existingDailyMenu);

        return existingDailyMenu;
    }
}