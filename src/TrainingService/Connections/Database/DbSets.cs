using Microsoft.EntityFrameworkCore;
using TrainingService.Exercises;
using TrainingService.Workouts;
using TrainingService.WorkoutSessions;
using TrainingService.WorkoutSessions.Common.ValueObjects;

namespace TrainingService.Connections.Database;

public partial class TrainingDbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    
    public DbSet<Workout> Workouts { get; set; }
}