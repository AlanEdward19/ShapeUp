﻿namespace NutritionService.Food.Common.Repository;

public interface IFoodMongoRepository
{
    Task<Food?> GetFoodByNameAsync(string name);
    Task<Food?> GetFoodByBarCodeAsync(string? barCode);
    Task InsertFoodAsync(Food food);
    Task UpdateFoodAsync(Food updatedFood);
    Task DeleteFoodAsync(string? barCode);
    Task<IEnumerable<Food>> ListUnrevisedFoodsAsync();
}