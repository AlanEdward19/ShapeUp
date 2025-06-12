using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserNutrition.DeleteUserNutrition;

public class DeleteUserNutritionCommandHandler(IUserNutritionMongoRepository repository) : 
    IHandler<UserNutrition, DeleteUserNutritionCommand>
{
    public async Task<UserNutrition> HandleAsync(DeleteUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var existingUserNutrition = await repository.GetUserNutritionDetailsAsync(item.Id);
        if (existingUserNutrition == null)
        {
            throw new NotFoundException($"UserNutrition with ID {item.Id} not found.");
        }

        // Delete the UserNutrition
        await repository.DeleteUserNutritionAsync(item.Id);

        return existingUserNutrition;
    }
}