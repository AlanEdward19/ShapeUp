namespace NutritionService.Dish.Common.Repository;

/// <summary>
/// Interface para o repositório de pratos no MongoDB.
/// </summary>
public interface IDishMongoRepository
{
    Task<Dish?> GetDishByIdAsync(string? id);
    Task InsertDishAsync(Dish dish);
    Task UpdateDishAsync(Dish updatedDish);
    Task DeleteDishAsync(string? id);

    Task<IEnumerable<Dish>> GetManyByIdsAsync(string[] dishIds, CancellationToken cancellationToken);
    Task<IEnumerable<Dish>> ListDishesAsync(int itemPage, int itemRows, CancellationToken cancellationToken, string userId);
}