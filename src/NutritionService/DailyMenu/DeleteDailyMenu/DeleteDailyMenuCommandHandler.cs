using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.DeleteDailyMenu;

public class DeleteDailyMenuCommandHandler(IDailyMenuMongoRepository repository) : 
    IHandler<DailyMenu, DeleteDailyMenuCommand>
{
    public async Task<DailyMenu> HandleAsync(DeleteDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var existingDailyMenu = await repository.GetDailyMenuDetailsAsync(item.Id);
        
        if (existingDailyMenu == null)
            throw new NotFoundException(item.Id);
        
        
        await repository.DeleteDailyMenuAsync(item.Id);

        return existingDailyMenu;
    }
}