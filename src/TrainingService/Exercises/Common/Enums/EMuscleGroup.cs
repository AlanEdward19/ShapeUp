namespace TrainingService.Exercises.Common.Enums;

[Flags]
public enum EMuscleGroup
{
    Chest = 1 << 0,
    UpperChest = 1 << 1,
    LowerChest = 1 << 2,
    
    Triceps = 1 << 3,
    Biceps = 1 << 4,
    Forearms = 1 << 5,
    
    DeltoidAnterior = 1 << 6,
    DeltoidLateral = 1 << 7,
    DeltoidPosterior = 1 << 8,
    Traps = 1 << 9,
    
    UpperBack = 1 << 10,
    MiddleBack = 1 << 11,
    LowerBack = 1 << 12,
    Lats = 1 << 13,
    
    AbsUpper = 1 << 14,
    AbsLower = 1 << 15,
    AbsObliques = 1 << 16,
    
    Quadriceps = 1 << 17,
    Hamstrings = 1 << 18,
    Glutes = 1 << 19,
    Calves = 1 << 20,
    HipFlexors = 1 << 21,
    
    FullBody = 1 << 22
}