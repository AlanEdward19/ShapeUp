﻿using MongoDB.Driver;
using NutritionService.Connections;
using SharedKernel.Utils;

namespace NutritionService.Meal.Common;

public class MealMongoRepository(NutritionDbContext context) : IMealMongoRepository
{
    /// <summary>
    /// Método para buscar uma refeição pelo ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Meal?> GetMealByIdAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return await context.Meals.Find(meal => meal.Id == id).SingleOrDefaultAsync();
    }

    /// <summary>
    /// Método para inserir uma refeição.
    /// </summary>
    /// <param name="meal"></param>
    public async Task InsertMealAsync(Meal meal)
    {
        await context.Meals.InsertOneAsync(meal);
    }

    /// <summary>
    /// Método para atualizar uma refeição.
    /// </summary>
    /// <param name="updatedMeal"></param>
    /// <returns></returns>
    public Task UpdateMealAsync(Meal updatedMeal)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, updatedMeal.Id);
        return context.Meals.ReplaceOneAsync(filter, updatedMeal);
    }

    /// <summary>
    /// Método para deletar uma refeição pelo ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task DeleteMealAsync(string? id)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
        return context.Meals.DeleteOneAsync(filter);
    }

    /// <summary>
    /// Método para buscar várias refeições por seus IDs.
    /// </summary>
    /// <param name="itemMealIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Meal>> GetManyMealsByIdsAsync(string[] itemMealIds, CancellationToken cancellationToken)
    {
        if (itemMealIds == null || itemMealIds.Length == 0)
        {
            return await Task.FromResult<IEnumerable<Meal>>([]);
        }

        var filter = Builders<Meal>.Filter.In(m => m.Id, itemMealIds);
        return await context.Meals.Find(filter).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Método para listar refeições com paginação.
    /// </summary>
    /// <param name="itemPage"></param>
    /// <param name="itemRows"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Meal>> ListMealsAsync(int itemPage, int itemRows, CancellationToken cancellationToken)
    {
        return await context.Meals.Find(meal => meal.CreatedBy == ProfileContext.ProfileId)
            .Skip(itemPage * itemRows)
            .Limit(itemRows)
            .ToListAsync(cancellationToken)
            .ContinueWith(task => task.Result.AsEnumerable(), cancellationToken);
    }
}