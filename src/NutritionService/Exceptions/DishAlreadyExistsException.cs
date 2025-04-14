namespace NutritionService.Exceptions;

/// <summary>
/// Exceção para quando um prato já existe
/// </summary>
public class DishAlreadyExistsException : Exception
{
    /// <summary>
    /// Construtor Padrão
    /// </summary>
    public DishAlreadyExistsException()
    { }

    /// <summary>
    /// Construtor com mensagem
    /// </summary>
    /// <param name="barcode"></param>
    public DishAlreadyExistsException(string? barcode) : base($"Dish with barcode '{barcode}' already exists")
    {
        
    }
}