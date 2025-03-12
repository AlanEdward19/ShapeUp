namespace SocialService.Common.ValueObjects;

/// <summary>
/// Classe base para os parametros de query
/// </summary>
public class BaseQueryParametersValueObject
{
    /// <summary>
    /// Número da página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Quantidade de linhas
    /// </summary>
    public int Rows { get; set; } = 10;
}