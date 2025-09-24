using MongoDB.Driver;
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
    public async Task UpdateMealAsync(Meal updatedMeal, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, updatedMeal.Id);
        await context.Meals.ReplaceOneAsync(filter, updatedMeal, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Método para deletar uma refeição pelo ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteMealAsync(string? id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
        await context.Meals.DeleteOneAsync(filter, cancellationToken);
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
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Meal>> ListMealsAsync(int itemPage, int itemRows, CancellationToken cancellationToken, string userId)
    {
        return await context.Meals.Find(meal => meal.UserId == userId)
            .Skip((itemPage - 1) * itemRows)
            .Limit(itemRows)
            .ToListAsync(cancellationToken);
    }
}