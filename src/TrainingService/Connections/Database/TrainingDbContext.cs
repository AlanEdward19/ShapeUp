using Microsoft.EntityFrameworkCore;
using TrainingService.Workouts;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.Connections.Database;

public partial class TrainingDbContext : DbContext
{
    public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.Exercises)
            .WithMany();
    }
    
}