namespace NutritionService.Exceptions;

/// <summary>
///     Exceção para quando um item não é encontrado
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    ///     Construtor com mensagem
    /// </summary>
    /// <param name="message"></param>
    public NotFoundException(string message) : base(message)
    {
    }
}