using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common.Interfaces;
using NutritionService.UserNutrition.CreateUserNutrition;
using NutritionService.UserNutrition.DeleteUserNutrition;
using NutritionService.UserNutrition.EditUserNutrition;
using NutritionService.UserNutrition.GetUserNutritionDetails;
using NutritionService.UserNutrition.ListManagedUserNutritions;
using NutritionService.UserNutrition.ListUserNutritions;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

namespace NutritionService.UserNutrition;

/// <summary>
/// Controlar para gerenciar a nutrição do usuário.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class UserNutritionController : ControllerBase
{
    /// <summary>
    /// Rota responsável por criar uma nutrição do usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{userId}")]
    [ProducesResponseType(typeof(UserNutritionDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUserNutrition(
        string userId,
        CreateUserNutritionCommand command,
        [FromServices] IHandler<UserNutritionDto, CreateUserNutritionCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetUserId(userId);
        //Validation
        
        return Created(HttpContext.Request.Path ,await handler.HandleAsync(command, cancellationToken));
    }
    
    /// <summary>
    /// Rota responsável por deletar uma nutrição do usuário.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUserNutrition(string id,
        [FromServices] IHandler<bool, DeleteUserNutritionCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation

        await handler.HandleAsync(new DeleteUserNutritionCommand(id), cancellationToken);
        
        return NoContent();
    }
    
    /// <summary>
    /// Rota responsável por editar uma nutrição do usuário.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditUserNutrition(string id,
        [FromBody] EditUserNutritionCommand command,
        [FromServices] IHandler<bool, EditUserNutritionCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetId(id);
        
        //Validation
        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }
    /// <summary>
    /// Rota responsável por obter os detalhes de uma nutrição do usuário.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("details/{id}")]
    [ProducesResponseType(typeof(UserNutritionDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserNutritionDetails(string id,
        [FromServices] IHandler<UserNutritionDto, GetUserNutritionDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation
        
        return Ok(await handler.HandleAsync(new GetUserNutritionDetailsQuery(id), cancellationToken));
    }

    /// <summary>
    /// Rota responsável por listar as nutrições do usuário.
    /// </summary>
    /// <param name="managerId"></param>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list/{managerId}")]
    [ProducesResponseType(typeof(IEnumerable<UserNutritionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListManagedUserNutritions(
        string managerId,
        [FromQuery] ListManagedUserNutritionsQuery query,
        [FromServices] IHandler<IEnumerable<UserNutritionDto>, ListManagedUserNutritionsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        query.SetNutritionManagerId(managerId);
        
        //Validation
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    /// <summary>
    /// Rota responsável por listar as nutrições de um usuário específico.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(UserNutritionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserNutrition(
        string userId,
        [FromServices] IHandler<IEnumerable<UserNutritionDto>, ListUserNutritionsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        var query = new ListUserNutritionsQuery();
        query.SetUserId(userId);


        var nutritions = await handler.HandleAsync(query, cancellationToken);
        var firstNutrition = nutritions.FirstOrDefault();

        if (firstNutrition == null)
        {
            return NotFound();
        }

        return Ok(firstNutrition);
    }
}