using MongoDB.Bson;
using MongoDB.Driver;
using NutritionService.Connections;

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
    /// <returns></returns>
    public async Task<IEnumerable<DailyMenu>> ListDailyMenusAsync(DayOfWeek? dayOfWeek, int page, int size)
    {
        var filter = dayOfWeek == null
            ? Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.DayOfWeek), BsonNull.Value)
            : Builders<DailyMenu>.Filter.Eq(nameof(DailyMenu.DayOfWeek), dayOfWeek);


        return await context.DailyMenus
            .Find(filter)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }

    /// <summary>
    /// Consulta completa sem filtro
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<DailyMenu>> ListDailyMenusAsync(int page, int size)
    {
        return await context.DailyMenus
            .Find(_ => true)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();
    }
}