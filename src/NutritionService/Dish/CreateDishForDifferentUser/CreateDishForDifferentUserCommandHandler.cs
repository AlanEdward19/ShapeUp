using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Dish.CreateDishForDifferentUser;

public class CreateDishForDifferentUserCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository userFoodRepository) : IHandler<DishDto, CreateDishForDifferentUserCommand>
{
    /// <summary>
    /// Handles the creation of a new dish based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DishDto> HandleAsync(CreateDishForDifferentUserCommand item, CancellationToken cancellationToken)
    {
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);
        
        var dish = new Dish(item.Name, builtFoods.ToList());
        dish.SetId();
        dish.SetCreatedBy(ProfileContext.ProfileId);
        dish.SetUserId(item.UserId);
        
        await dishRepository.InsertDishAsync(dish);

        return new DishDto(dish);
    }
}