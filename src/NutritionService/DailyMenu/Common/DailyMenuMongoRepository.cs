using MongoDB.Bson;
using MongoDB.Driver;
using NutritionService.Connections;
using SharedKernel.Utils;

namespace NutritionService.DailyMenu.Common;

/// <summary>
/// Repositório para gerenciar o acesso aos dados do DailyMenu.
/// </summary>
public class DailyMenuMongoRepository(NutritionDbContext context) : IDailyMenuMongoRepository
{
    /// <summary>
    /// Método para buscar um DailyMenu pelo ID.
    /// </summary>
    /// <param name="dailyMenuId"></param>
    /// <returns></returns>
    public async Task<DailyMenu?> GetDailyMenuDetailsAsync(string? dailyMenuId)
    {
        if (string.IsNullOrWhiteSpace(dailyMenuId)) return null;
        return await context.DailyMenus.Find(d => d.Id == dailyMenuId).SingleOrDefaultAsync();
    }

    /// <summary>
    /// Método para inserir um DailyMenu.
    /// </summary>
    /// <param name="dailyMenu"></param>
    public async Task InsertDailyMenuAsync(DailyMenu dailyMenu)
    {
        ArgumentNullException.ThrowIfNull(dailyMenu);
        await context.DailyMenus.InsertOneAsync(dailyMenu);
    }

    /// <summary>
    /// Método para atualizar um DailyMenu.
    /// </summary>
    /// <param name="updatedDailyMenu"></param>
    public async Task UpdateDailyMenuAsync(DailyMenu updatedDailyMenu)
    {
        ArgumentNullException.ThrowIfNull(updatedDailyMenu);
        var filter = Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.Id), updatedDailyMenu.Id);
        await context.DailyMenus.ReplaceOneAsync(filter, updatedDailyMenu);
    }

    /// <summary>
    /// Método para deletar um DailyMenu.
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteDailyMenuAsync(string? id)
    {
        if (string.IsNullOrWhiteSpace(id)) return;
        var filter = Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.Id), id);
        await context.DailyMenus.DeleteOneAsync(filter);
    }


    /// <summary>
    /// Consulta filtrada, com paginação e possibilidade de buscar menus sem dia definido
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<DailyMenu>> ListDailyMenusAsync(DayOfWeek? dayOfWeek, int page, int size,
        string userId)
    {
        var filters = new List<FilterDefinition<DailyMenu>>
        {
            // Filtro por dia da semana (pode ser nulo)
            dayOfWeek == null
                ? Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.DayOfWeek), BsonNull.Value)
                : Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.DayOfWeek), dayOfWeek),
            // Filtro por usuário atual
            Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.UserId), userId)
        };

        // Combinação dos filtros
        var combinedFilter = Builders<DailyMenu>.Filter.And(filters);


        return await context.DailyMenus
            .Find(combinedFilter)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    /// <summary>
    /// Consulta completa sem filtro
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<DailyMenu>> ListDailyMenusAsync(int page, int size, string userId)
    {
        return await context.DailyMenus
            .Find(dm=>dm.UserId == userId)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    public async Task<IEnumerable<DailyMenu>> GetManyByIdsAsync(string[] itemDailyMenuIds, CancellationToken cancellationToken)
    {
        if (itemDailyMenuIds == null || itemDailyMenuIds.Length == 0)
        {
            return await Task.FromResult<IEnumerable<DailyMenu>>(Array.Empty<DailyMenu>());
        }

        var filter = Builders<DailyMenu>.Filter.In(d => d.Id, itemDailyMenuIds);
        return await context.DailyMenus.Find(filter).ToListAsync(cancellationToken);
    }
}