using Microsoft.EntityFrameworkCore;
using TrainingService.Exercises;
using TrainingService.Workouts;

namespace TrainingService.Connections.Database;

public partial class TrainingDbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    
    public DbSet<Workout> Workouts { get; set; }
}