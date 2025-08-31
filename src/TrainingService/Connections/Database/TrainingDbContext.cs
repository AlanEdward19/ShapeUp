using Microsoft.EntityFrameworkCore;
using TrainingService.Exercises;
using TrainingService.Exercises.Common.Enums;
using TrainingService.Workouts;

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
        
        modelBuilder.Entity<Exercise>().HasData(
            
            // ---------- PEITO ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96400"), "Supino Reto",
                EMuscleGroup.MiddleChest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96401"), "Supino Inclinado",
                EMuscleGroup.UpperChest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96402"), "Supino Declinado",
                EMuscleGroup.LowerChest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96403"), "Supino com Halteres",
                EMuscleGroup.MiddleChest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96404"), "Crucifixo com Halteres",
                EMuscleGroup.MiddleChest | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96405"), "Crucifixo no Cabo (Peck-Deck/Crossover)",
                EMuscleGroup.MiddleChest | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96406"), "Flexão de Braço",
                EMuscleGroup.Chest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96407"), "Flexão Diamante",
                EMuscleGroup.MiddleChest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, false, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96408"), "Supino Fechado",
                EMuscleGroup.MiddleChest | EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),

// ---------- COSTAS ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96409"), "Puxada na Frente (Pulldown)",
                EMuscleGroup.Lats | EMuscleGroup.UpperBack | EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null,
                null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9640a"), "Barra Fixa (Pronada)",
                EMuscleGroup.Lats | EMuscleGroup.UpperBack | EMuscleGroup.Biceps | EMuscleGroup.Forearms |
                EMuscleGroup.Traps, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9640b"), "Barra Fixa (Supinada)",
                EMuscleGroup.Lats | EMuscleGroup.Biceps | EMuscleGroup.UpperBack | EMuscleGroup.Forearms, false, null,
                null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9640c"), "Remada Curvada com Barra",
                EMuscleGroup.MiddleBack | EMuscleGroup.Lats | EMuscleGroup.LowerBack | EMuscleGroup.Biceps |
                EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9640d"), "Remada Unilateral com Halter",
                EMuscleGroup.Lats | EMuscleGroup.MiddleBack | EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null,
                null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9640e"), "Remada Baixa (Cabo)",
                EMuscleGroup.MiddleBack | EMuscleGroup.Lats | EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null,
                null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9640f"), "Pullover (Cabo/Halter)",
                EMuscleGroup.Lats | EMuscleGroup.UpperBack | EMuscleGroup.MiddleChest, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96410"), "Levantamento Terra",
                EMuscleGroup.LowerBack | EMuscleGroup.Hamstrings | EMuscleGroup.Glutes | EMuscleGroup.Traps |
                EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- OMBROS ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96411"), "Desenvolvimento Militar (Barra)",
                EMuscleGroup.DeltoidAnterior | EMuscleGroup.DeltoidLateral | EMuscleGroup.Triceps | EMuscleGroup.Traps,
                true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96412"), "Desenvolvimento com Halteres",
                EMuscleGroup.DeltoidAnterior | EMuscleGroup.DeltoidLateral | EMuscleGroup.Triceps, true, null, null,
                new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96413"), "Elevação Lateral",
                EMuscleGroup.DeltoidLateral | EMuscleGroup.Traps, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96414"), "Elevação Frontal",
                EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96415"), "Elevação Posterior (Peck-Deck Inverso)",
                EMuscleGroup.DeltoidPosterior | EMuscleGroup.Traps | EMuscleGroup.UpperBack, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96416"), "Face Pull",
                EMuscleGroup.DeltoidPosterior | EMuscleGroup.Traps | EMuscleGroup.UpperBack, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96417"), "Remada Alta",
                EMuscleGroup.DeltoidLateral | EMuscleGroup.Traps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96418"), "Encolhimento",
                EMuscleGroup.Traps | EMuscleGroup.UpperBack | EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- BÍCEPS ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96419"), "Rosca Direta (Barra)",
                EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9641a"), "Rosca Alternada (Halteres)",
                EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9641b"), "Rosca Martelo",
                EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9641c"), "Rosca Scott",
                EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9641d"), "Rosca Inversa",
                EMuscleGroup.Forearms | EMuscleGroup.Biceps, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9641e"), "Rosca Concentrada",
                EMuscleGroup.Biceps, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- TRÍCEPS ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9641f"), "Tríceps Testa",
                EMuscleGroup.Triceps | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96420"), "Tríceps Corda",
                EMuscleGroup.Triceps | EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96421"), "Tríceps Francês (Halter)",
                EMuscleGroup.Triceps, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96422"), "Paralelas (Dips)",
                EMuscleGroup.Triceps | EMuscleGroup.MiddleChest | EMuscleGroup.DeltoidAnterior, false, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96423"), "Kickback de Tríceps",
                EMuscleGroup.Triceps | EMuscleGroup.DeltoidPosterior, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- PERNAS / GLÚTEOS ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96424"), "Agachamento Livre",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Hamstrings | EMuscleGroup.LowerBack, true,
                null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96425"), "Agachamento Frontal (Front Squat)",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Hamstrings | EMuscleGroup.UpperBack, true,
                null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96426"), "Hack Squat",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96427"), "Leg Press 45°",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Hamstrings, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96428"), "Passada (Avanço)",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Hamstrings, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96429"), "Cadeira Extensora",
                EMuscleGroup.Quadriceps, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9642a"), "Mesa Flexora",
                EMuscleGroup.Hamstrings, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9642b"), "Stiff (Terra Romeno)",
                EMuscleGroup.Hamstrings | EMuscleGroup.Glutes | EMuscleGroup.LowerBack, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9642c"), "Levantamento Terra Sumo",
                EMuscleGroup.Glutes | EMuscleGroup.Hamstrings | EMuscleGroup.Quadriceps | EMuscleGroup.Traps, true,
                null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9642d"), "Elevação Pélvica",
                EMuscleGroup.Glutes | EMuscleGroup.Hamstrings | EMuscleGroup.LowerBack, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9642e"), "Subida no Banco",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Hamstrings | EMuscleGroup.Calves, true,
                null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9642f"), "Panturrilha em Pé (Máquina)",
                EMuscleGroup.Calves, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96430"), "Panturrilha Sentado",
                EMuscleGroup.Calves, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96431"), "Elevação de Quadril no Cabo",
                EMuscleGroup.Glutes | EMuscleGroup.Hamstrings, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96432"), "Flexão de Quadril (Cabo)",
                EMuscleGroup.HipFlexors | EMuscleGroup.AbsLower, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- ABDÔMEN / CORE ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96433"), "Abdominal Supra",
                EMuscleGroup.AbsUpper, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96434"), "Abdominal Infra",
                EMuscleGroup.AbsLower | EMuscleGroup.HipFlexors, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96435"), "Elevação de Pernas",
                EMuscleGroup.AbsLower | EMuscleGroup.HipFlexors, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96436"), "Prancha",
                EMuscleGroup.AbsUpper | EMuscleGroup.AbsLower | EMuscleGroup.AbsObliques, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96437"), "Prancha Lateral",
                EMuscleGroup.AbsObliques | EMuscleGroup.AbsLower | EMuscleGroup.AbsUpper, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96438"), "Ab Wheel (Roda)",
                EMuscleGroup.AbsUpper | EMuscleGroup.AbsLower | EMuscleGroup.AbsObliques | EMuscleGroup.HipFlexors,
                true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96439"), "Russian Twist",
                EMuscleGroup.AbsObliques | EMuscleGroup.AbsUpper, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9643a"), "Mountain Climbers",
                EMuscleGroup.AbsLower | EMuscleGroup.AbsObliques | EMuscleGroup.HipFlexors, false, null, null, new DateTime(2025,08,30),
                new DateTime(2025,08,30)),

// ---------- ANTEBRAÇO / PEGADA ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9643b"), "Rosca de Punho",
                EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9643c"), "Rosca de Punho Inversa",
                EMuscleGroup.Forearms, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9643d"), "Farmer's Walk",
                EMuscleGroup.Forearms | EMuscleGroup.Traps | EMuscleGroup.Glutes | EMuscleGroup.Quadriceps |
                EMuscleGroup.Hamstrings | EMuscleGroup.Calves, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- CONDICIONAMENTO ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9643e"), "Burpee",
                EMuscleGroup.FullBody, false, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc9643f"), "Kettlebell Swing",
                EMuscleGroup.Glutes | EMuscleGroup.Hamstrings | EMuscleGroup.LowerBack | EMuscleGroup.DeltoidAnterior |
                EMuscleGroup.AbsLower, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96440"), "Clean and Press (Haltere/Barra)",
                EMuscleGroup.FullBody | EMuscleGroup.Traps | EMuscleGroup.DeltoidAnterior | EMuscleGroup.Triceps |
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),

// ---------- VARIAÇÕES ----------
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96441"), "Puxada Neutra (Triângulo)",
                EMuscleGroup.Lats | EMuscleGroup.MiddleBack | EMuscleGroup.Biceps | EMuscleGroup.Forearms, true, null,
                null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96442"), "Remada Cavalinho (T-Bar)",
                EMuscleGroup.MiddleBack | EMuscleGroup.Lats | EMuscleGroup.Biceps | EMuscleGroup.LowerBack, true, null,
                null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96443"), "Crucifixo Inclinado",
                EMuscleGroup.UpperChest | EMuscleGroup.DeltoidAnterior, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96444"),
                "Pulldown com Barra Reta (Pulldown no Cabo)",
                EMuscleGroup.Lats | EMuscleGroup.UpperBack | EMuscleGroup.Traps, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96445"), "Desenvolvimento Arnold",
                EMuscleGroup.DeltoidAnterior | EMuscleGroup.DeltoidLateral | EMuscleGroup.Triceps, true, null, null,
                new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96446"), "Good Morning",
                EMuscleGroup.LowerBack | EMuscleGroup.Hamstrings | EMuscleGroup.Glutes, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96447"), "Box Jump",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Calves | EMuscleGroup.Hamstrings, false,
                null, null, new DateTime(2025,08,30), new DateTime(2025,08,30)),
            new Exercise(Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc96448"), "Afundo Búlgaro",
                EMuscleGroup.Quadriceps | EMuscleGroup.Glutes | EMuscleGroup.Hamstrings, true, null, null, new DateTime(2025,08,30), new DateTime(2025,08,30))
        );
    }
}