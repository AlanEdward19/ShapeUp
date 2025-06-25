using Microsoft.EntityFrameworkCore;

namespace ProfessionalManagementService.Connections.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}