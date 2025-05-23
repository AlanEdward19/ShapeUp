﻿using TrainingService.Common.Interfaces;
using TrainingService.Exercises.Common.Enums;
using TrainingService.Exercises.Common.Repository;

namespace TrainingService.Exercises.UpdateExercise;

/// <summary>
///     Handler para atualizar um exercício
/// </summary>
/// <param name="repository"></param>
public class UpdateExerciseCommandHandler(IExerciseRepository repository)
    : IHandler<bool, UpdateExerciseCommand>
{
    public async Task<bool> HandleAsync(UpdateExerciseCommand command, CancellationToken cancellationToken)
    {
        Exercise? exercise = await repository.GetExerciseAsync(command.Id, cancellationToken);
        ArgumentNullException.ThrowIfNull(exercise);
        
        exercise.UpdateExercise(command);
        
        await repository.UpdateAsync(exercise, cancellationToken);
        
        return true;
    }
}