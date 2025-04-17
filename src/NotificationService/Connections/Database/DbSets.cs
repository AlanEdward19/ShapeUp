using Microsoft.EntityFrameworkCore;

namespace NotificationService.Connections.Database;

public partial class NotificationDbContext
{
    public DbSet<User.User> Users { get; set; }
}