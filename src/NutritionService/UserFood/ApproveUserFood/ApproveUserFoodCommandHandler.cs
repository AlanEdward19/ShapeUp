using NutritionService.Common.Interfaces;
using NutritionService.PublicFood.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.ApproveUserFood;

public class ApproveUserFoodCommandHandler(IUserFoodMongoRepository userFoodRepository, IPublicFoodMongoRepository publicFoodRepository) : IHandler<Food, ApproveUserFoodCommand>
{
    public async Task<Food> HandleAsync(ApproveUserFoodCommand command, CancellationToken cancellationToken)
    {
        var existingFood = await userFoodRepository.GetUserFoodByIdAsync(command.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{command.Id}' not found");
        
        existingFood.MarkAsRevised();
        
        await publicFoodRepository.CreatePublicFoodAsync(existingFood);
        
        await userFoodRepository.DeleteUserFoodAsync(command.Id);

        return existingFood;
    }
}