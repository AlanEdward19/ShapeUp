﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProfessionalManagementService.Configuration.Filters;

/// <summary>
///     Filtro para substituir a versão pela versão exata no path
/// </summary>
public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
{
    /// <summary>
    ///     Método para aplicar o filtro
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths;

        swaggerDoc.Paths = new OpenApiPaths();

        foreach (var path in paths)
        {
            var key = path.Key.Replace("v{version}", swaggerDoc.Info.Version);

            var value = path.Value;

            swaggerDoc.Paths.Add(key, value);
        }
    }
}