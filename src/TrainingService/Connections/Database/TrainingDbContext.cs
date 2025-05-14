using Microsoft.EntityFrameworkCore;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.Connections.Database;

public partial class TrainingDbContext : DbContext
{
    public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
    {
    }
    
}