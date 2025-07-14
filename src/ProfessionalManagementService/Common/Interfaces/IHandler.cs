namespace ProfessionalManagementService.Common.Interfaces;

/// <summary>
///     Interface para padronizar handlers.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TItem"></typeparam>
public interface IHandler<TResponse, in TItem>
{
    /// <summary>
    ///     Método para lidar com operações.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse> HandleAsync(TItem item, CancellationToken cancellationToken);
}