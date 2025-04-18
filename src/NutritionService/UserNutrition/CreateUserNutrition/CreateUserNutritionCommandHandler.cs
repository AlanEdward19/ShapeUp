using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.Common.Repository;

namespace NutritionService.UserNutrition.CreateUserNutrition;

public class CreateUserNutritionCommandHandler(IUserNutritionMongoRepository repository) : 
    IHandler<UserNutrition, CreateUserNutritionCommand>
{
    public async Task<UserNutrition> HandleAsync(CreateUserNutritionCommand item, CancellationToken cancellationToken)
    {
        var userNutrition = item.ToUserNutrition();
        
        await repository.InsertUserNutritionAsync(userNutrition);

        return userNutrition;
    }
}