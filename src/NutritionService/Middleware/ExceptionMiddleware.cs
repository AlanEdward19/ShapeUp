﻿using System.Net;
using System.Text.Json;
using NutritionService.Exceptions;
using SharedKernel.Exceptions;

namespace NutritionService.Middleware;

/// <summary>
///     Middleware para tratamento de exceções
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger /*, DatabaseContext databaseContext*/)
{
    /// <summary>
    ///     Método para invocar o middleware
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            // if (databaseContext.Database.CurrentTransaction != null)
            //     await databaseContext.Database.RollbackTransactionAsync();

            var response = context.Response;
            response.ContentType = "application/json";

            #region Status Code

            switch (error)
            {
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    logger.LogWarning($"[Resource not found request] {error.Message}");
                    break;

                case NotFoundException e:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    logger.LogError($"[Not Found request]: {e.Message} {e.StackTrace}");
                    break;
                
                case FoodAlreadyExistsException e:
                    response.StatusCode = (int) HttpStatusCode.Conflict;
                    logger.LogError($"[Conflict request]: {e.Message} {e.StackTrace}");
                    break;

                case UnauthorizedAccessException e:
                    response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    logger.LogWarning($"[Unauthorized request] {error.Message}");
                    break;

                default:
                    // unhandled error
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    logger.LogError($"[Internal error request] {error.Message}");
                    break;
            }

            #endregion

            #region Build Error Message

            var result = JsonSerializer.Serialize(new
            {
                type = error.GetType().ToString(),
                title = error.GetType().Name,
                status = response.StatusCode,
                error = error.Message,
                occuredAt = DateTime.UtcNow
            });

            #endregion

            await response.WriteAsync(result);
        }
    }
}