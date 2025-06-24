using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.DeleteDailyMenu;

/// <summary>
/// Handles the deletion of a daily menu.
/// </summary>
/// <param name="repository"></param>
public class DeleteDailyMenuCommandHandler(IDailyMenuMongoRepository repository) : 
    IHandler<bool, DeleteDailyMenuCommand>
{
    /// <summary>
    /// Handles the deletion of a daily menu by its ID.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(DeleteDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var existingDailyMenu = await repository.GetDailyMenuDetailsAsync(item.Id);
        
        if (existingDailyMenu == null)
            throw new NotFoundException(item.Id);
        
        
        await repository.DeleteDailyMenuAsync(item.Id);

        return true;
    }
}