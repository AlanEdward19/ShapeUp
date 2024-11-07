using Microsoft.EntityFrameworkCore;

namespace SocialService.Database.Sql;

public partial class DatabaseContext
{
    /// <summary>
    /// Perfis de usuários.
    /// </summary>
    public DbSet<Profile.Profile> Profiles { get; set; }
}