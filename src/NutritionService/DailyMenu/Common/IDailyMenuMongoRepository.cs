namespace NutritionService.DailyMenu.Common;

/// <summary>
/// Interface para o repositório de DailyMenu.
/// </summary>
public interface IDailyMenuMongoRepository
{
    /// <summary>
    /// Método para obter um DailyMenu pelo seu ID.
    /// </summary>
    /// <param name="dailyMenuId"></param>
    /// <returns></returns>
    Task<DailyMenu?> GetDailyMenuDetailsAsync(string? dailyMenuId);
    /// <summary>
    /// Método para inserir um DailyMenu.
    /// </summary>
    /// <param name="dailyMenu"></param>
    /// <returns></returns>
    Task InsertDailyMenuAsync(DailyMenu dailyMenu);
    /// <summary>
    /// Método para atualizar um DailyMenu.
    /// </summary>
    /// <param name="updatedDailyMenu"></param>
    /// <returns></returns>
    Task UpdateDailyMenuAsync(DailyMenu updatedDailyMenu);
    /// <summary>
    /// Método para deletar um DailyMenu.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteDailyMenuAsync(string? id);
    /// <summary>
    /// Método para listar os DailyMenus, filtrando por dia da semana.
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    Task<IEnumerable<DailyMenu>> ListDailyMenusAsync(DayOfWeek? dayOfWeek, int page, int size);
    /// <summary>
    /// Método para listar os DailyMenus, sem filtro.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    Task<IEnumerable<DailyMenu>> ListDailyMenusAsync(int page, int size);

    Task<IEnumerable<DailyMenu>> GetManyByIdsAsync(string[] itemDailyMenuIds, CancellationToken cancellationToken);
}