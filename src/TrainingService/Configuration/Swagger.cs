namespace TrainingService.Configuration;

/// <summary>
///     Classe para configuração do Swagger
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    ///     Método para configurar o Swagger
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
        return app;
    }
}