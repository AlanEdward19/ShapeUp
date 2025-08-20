using TrainingService.Exercises.Common.Enums;

namespace TrainingService.Exercises.Common.Repository;

/// <summary>
/// Interface para o repositório de exercícios, definindo os métodos necessários para gerenciar operações relacionadas a exercícios.
/// </summary>
public interface IExerciseRepository
{
    /// <summary>
    /// Método para verificar se um exercício existe pelo ID.
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ExerciseExistsAsync(Guid exerciseId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para verificar se um ou mais exercícios existem por uma lista de IDs.
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExerciseExistsAsync(ICollection<Guid> exerciseId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para obter um exercício por ID.
    /// </summary>
    /// <param name="exerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Exercise> GetExerciseByIdAsync(Guid exerciseId, CancellationToken cancellationToken);

    /// <summary>
    /// Método para obter exercícios por uma lista de IDs.
    /// </summary>
    /// <param name="exerciseIds"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="track"></param>
    /// <returns></returns>
    Task<ICollection<Exercise>> GetExercisesByIdsAsync(List<Guid> exerciseIds, CancellationToken cancellationToken,
        bool track = false);
    
    /// <summary>
    /// Método para obter exercícios por grupo muscular.
    /// </summary>
    /// <param name="muscleGroup"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<Exercise>> GetExercisesByMuscleGroupAsync(EMuscleGroup muscleGroup, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para obter todos os exercícios.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<Exercise>> GetExercisesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para adicionar um novo exercício.
    /// </summary>
    /// <param name="exercise"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(Exercise exercise, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para excluir um exercício.
    /// </summary>
    /// <param name="exercise"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Exercise exercise, CancellationToken cancellationToken);
    
    /// <summary>
    /// Método para atualizar um exercício.
    /// </summary>
    /// <param name="exercise"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(Exercise exercise, CancellationToken cancellationToken);
}