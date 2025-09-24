using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.EditUserNutrition;

/// <summary>
/// Handles the editing of user nutrition details.
/// </summary>
public class EditUserNutritionCommandHandler : IHandler<bool, EditUserNutritionCommand>
{
    private readonly IUserNutritionMongoRepository _userNutritionRepository;

    // A dependência do dailyMenuRepository não é mais necessária.
    public EditUserNutritionCommandHandler(IUserNutritionMongoRepository userNutritionRepository)
    {
        _userNutritionRepository = userNutritionRepository;
    }

    /// <summary>
    /// Handles the editing of user nutrition details based on the provided command.
    /// </summary>
    public async Task<bool> HandleAsync(EditUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var existingUserNutrition = await _userNutritionRepository.GetUserNutritionDetailsAsync(item.Id);
        
        if (existingUserNutrition == null)
            throw new NotFoundException($"UserNutrition with id '{item.Id}' not found");

        // Atualiza a entidade diretamente com a lista de IDs do comando.
        existingUserNutrition.UpdateInfo(item.NutritionManagerId, item.DailyMenuIds);
        
        await _userNutritionRepository.UpdateUserNutritionAsync(existingUserNutrition);

        return true;
    }
}