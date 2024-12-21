namespace NutritionService.Exceptions;

/// <summary>
///     Exceção para quando um alimento já existe
/// </summary>
public class FoodAlreadyExistsException : Exception
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public FoodAlreadyExistsException()
    {
    }

    /// <summary>
    ///     Construtor com mensagem
    /// </summary>
    /// <param name="message"></param>
    public FoodAlreadyExistsException(string barcode) : base($"Food with barcode '{barcode}' already exists")
    {
    }
}