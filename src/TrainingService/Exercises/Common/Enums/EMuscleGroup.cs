namespace TrainingService.Exercises.Common.Enums;

[Flags]
public enum EMuscleGroup
{
    Chest = UpperChest | MiddleChest | LowerChest,
    MiddleChest = 1 << 0,
    UpperChest = 1 << 1,
    LowerChest = 1 << 2,
    
    Arms = Triceps | Biceps | Forearms,
    Triceps = 1 << 3,
    Biceps = 1 << 4,
    Forearms = 1 << 5,
    
    Shoulders = DeltoidAnterior | DeltoidLateral | DeltoidPosterior,
    DeltoidAnterior = 1 << 6,
    DeltoidLateral = 1 << 7,
    DeltoidPosterior = 1 << 8,
    
    Back = Traps | UpperBack | MiddleBack | LowerBack | Lats,
    Traps = 1 << 9,
    UpperBack = 1 << 10,
    MiddleBack = 1 << 11,
    LowerBack = 1 << 12,
    Lats = 1 << 13,
    
    Abs = AbsUpper | AbsLower | AbsObliques,
    AbsUpper = 1 << 14,
    AbsLower = 1 << 15,
    AbsObliques = 1 << 16,
    
    Legs = Quadriceps | Hamstrings | Glutes | Calves | HipFlexors,
    Quadriceps = 1 << 17,
    Hamstrings = 1 << 18,
    Glutes = 1 << 19,
    Calves = 1 << 20,
    HipFlexors = 1 << 21,
    
    FullBody = Chest | Arms | Shoulders | Back | Abs | Legs
}