using SocialService.Middleware;

namespace SocialService.Configuration;

/// <summary>
///     Classe para configuração de middlewares
/// </summary>
public static class MiddlewareConfiguration
{
    /// <summary>
    ///     Método para configurar os middlewares
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder ConfigureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}