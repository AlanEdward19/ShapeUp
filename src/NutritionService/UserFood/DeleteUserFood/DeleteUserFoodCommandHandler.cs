﻿using NutritionService.Common;
using NutritionService.Common.Interfaces;
using NutritionService.UserFood.Common.Repository;
using SharedKernel.Exceptions;

namespace NutritionService.UserFood.DeleteUserFood;

public class DeleteUserFoodCommandHandler(IUserFoodMongoRepository repository)
: IHandler<bool, DeleteUserFoodCommand>
{
    public async Task<bool> HandleAsync(DeleteUserFoodCommand item, CancellationToken cancellationToken)
    {
        var existingFood = await repository.GetUserFoodByIdAsync(item.Id);
        
        if (existingFood == null)
            throw new NotFoundException($"Food with id '{item.Id}' not found");
        
        await repository.DeleteUserFoodAsync(item.Id);
        return true;
    }
}