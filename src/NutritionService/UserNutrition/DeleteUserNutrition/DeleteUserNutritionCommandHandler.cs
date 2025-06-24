using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.DeleteUserNutrition;

/// <summary>
/// Handles the deletion of a user's nutrition record.
/// </summary>
/// <param name="repository"></param>
public class DeleteUserNutritionCommandHandler(IUserNutritionMongoRepository repository) : 
    IHandler<bool, DeleteUserNutritionCommand>
{
    /// <summary>
    /// Handles the deletion of a user's nutrition record based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(DeleteUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var existingUserNutrition = await repository.GetUserNutritionDetailsAsync(item.Id);
        if (existingUserNutrition == null)
        {
            throw new NotFoundException($"UserNutrition with ID {item.Id} not found.");
        }

        // Delete the UserNutrition
        await repository.DeleteUserNutritionAsync(item.Id);

        return true;
    }
}