using Microsoft.EntityFrameworkCore;

namespace SocialService.Database.Sql;

public partial class DatabaseContext
{
    public DbSet<Profile.Profile> Profiles { get; set; }
}