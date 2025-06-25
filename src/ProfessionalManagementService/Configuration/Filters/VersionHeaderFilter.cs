using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProfessionalManagementService.Configuration.Filters;

/// <summary>
///     Filtro para remover o parâmetro de versão da documentação
/// </summary>
public class VersionHeaderFilter : IOperationFilter
{
    /// <summary>
    ///     Método para aplicar o filtro
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters != null)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");

            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);
        }
    }
}