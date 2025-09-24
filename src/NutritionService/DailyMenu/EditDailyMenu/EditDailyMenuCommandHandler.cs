using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.Common;
using SharedKernel.Exceptions;

namespace NutritionService.DailyMenu.EditDailyMenu;

/// <summary>
/// Handles the editing of an existing daily menu.
/// </summary>
public class EditDailyMenuCommandHandler : IHandler<bool, EditDailyMenuCommand>
{
    private readonly IDailyMenuMongoRepository _dailyMenuRepository;

    // O mealRepository não é mais necessário aqui.
    public EditDailyMenuCommandHandler(IDailyMenuMongoRepository dailyMenuRepository)
    {
        _dailyMenuRepository = dailyMenuRepository;
    }

    /// <summary>
    /// Handles the editing of an existing daily menu based on the provided command.
    /// </summary>
    public async Task<bool> HandleAsync(EditDailyMenuCommand item, CancellationToken cancellationToken)
    {
        var existingDailyMenu = await _dailyMenuRepository.GetDailyMenuDetailsAsync(item.Id);
        
        if (existingDailyMenu == null)
            throw new NotFoundException($"DailyMenu with id '{item.Id}' not found");
        
        // Usa diretamente a lista de IDs do comando.
        existingDailyMenu.UpdateInfo(item.DayOfWeek, item.MealIds);
        
        await _dailyMenuRepository.UpdateDailyMenuAsync(existingDailyMenu);

        return true;
    }
}