using Microsoft.EntityFrameworkCore;

namespace SocialService.Connections.Sql;

/// <summary>
///     Contexto do banco de dados.
/// </summary>
public partial class DatabaseContext : DbContext
{
    /// <summary>
    ///     Construtor
    /// </summary>
    /// <param name="options"></param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}