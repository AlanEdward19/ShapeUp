using NutritionService.Common.Interfaces;
using NutritionService.Dish.Common.Repository;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Utils;

namespace NutritionService.Dish.CreateDishForSameUser;

/// <summary>
/// Handles the creation of a new dish.
/// </summary>
/// <param name="dishRepository"></param>
/// <param name="userFoodRepository"></param>
public class CreateDishForSameUserCommandHandler(IDishMongoRepository dishRepository, IUserFoodMongoRepository userFoodRepository) : IHandler<DishDto, CreateDishForSameUserCommand>
{
    /// <summary>
    /// Handles the creation of a new dish based on the provided command.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DishDto> HandleAsync(CreateDishForSameUserCommand item, CancellationToken cancellationToken)
    {
        var builtFoods = await userFoodRepository.GetManyByIdsAsync(item.FoodIds, cancellationToken);
        
        var dish = new Dish(item.Name, builtFoods.ToList());
        dish.SetId();
        dish.SetCreatedBy(ProfileContext.ProfileId);
        dish.SetUserId(ProfileContext.ProfileId);
        
        await dishRepository.InsertDishAsync(dish);

        return new DishDto(dish);
    }
}