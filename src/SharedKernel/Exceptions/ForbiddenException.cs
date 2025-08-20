namespace SharedKernel.Exceptions;

/// <summary>
///     Exceção para quando um usuário não tem permissão para acessar um recurso ou realizar uma ação.
/// </summary>
public class ForbiddenException : Exception
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public ForbiddenException()
    {
    }

    /// <summary>
    ///     Construtor com mensagem
    /// </summary>
    /// <param name="message"></param>
    public ForbiddenException(string message) : base(message)
    {
    }
}