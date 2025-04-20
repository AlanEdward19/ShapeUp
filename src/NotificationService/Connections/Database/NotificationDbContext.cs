using Microsoft.EntityFrameworkCore;

namespace NotificationService.Connections.Database;

public partial class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : DbContext(options)
{
    
}