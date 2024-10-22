using Microsoft.EntityFrameworkCore;

namespace SocialService.Database.Sql;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
}