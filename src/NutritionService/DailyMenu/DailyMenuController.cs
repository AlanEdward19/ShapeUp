using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NutritionService.Common.Interfaces;
using NutritionService.DailyMenu.CreateDailyMenuForDifferentUser;
using NutritionService.DailyMenu.CreateDailyMenuForSameUser;
using NutritionService.DailyMenu.DeleteDailyMenu;
using NutritionService.DailyMenu.EditDailyMenu;
using NutritionService.DailyMenu.GetDailyMenuDetails;
using NutritionService.DailyMenu.ListDailyMenus;
using SharedKernel.Filters;
using SharedKernel.Utils;
using ProfileContext = SharedKernel.Utils.ProfileContext;

namespace NutritionService.DailyMenu;


/// <summary>
/// Controlador para gerenciar o cardápio diário.
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class DailyMenuController : ControllerBase
{
    /// <summary>
    /// Rota para criar um novo cardápio diário para o usuário logado.
    /// </summary>
    /// <param name="forSameUserCommand"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(DailyMenuDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDailyMenuForSameUser
    (CreateDailyMenuForSameUserCommand forSameUserCommand,
        [FromServices] IHandler<DailyMenuDto, CreateDailyMenuForSameUserCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        // Validators
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(forSameUserCommand, cancellationToken));
    }

    /// <summary>
    /// Rota para Criar um cardápio diário para outro usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="forDifferentUserCommand"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{userId}")]
    [ProducesResponseType(typeof(DailyMenuDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDailyMenuForDifferentUser
    (
        string userId,
        CreateDailyMenuForDifferentUserCommand forDifferentUserCommand,
        [FromServices] IHandler<DailyMenuDto, CreateDailyMenuForDifferentUserCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        forDifferentUserCommand.SetUserId(userId);
        // Validators
        
        return Created(HttpContext.Request.Path, await handler.HandleAsync(forDifferentUserCommand, cancellationToken));
    }
    
    /// <summary>
    /// Rota para deletar um cardápio diário.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteDailyMenu(string id,
        [FromServices] IHandler<bool, DeleteDailyMenuCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validators

        await handler.HandleAsync(new DeleteDailyMenuCommand(id), cancellationToken);

        return NoContent();
    }
    /// <summary>
    /// Rota para editar um cardápio diário.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditDailyMenu(string id, [FromBody] EditDailyMenuCommand command,
        [FromServices] IHandler<bool, EditDailyMenuCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        command.SetId(id);

        await handler.HandleAsync(command, cancellationToken);
        
        //Validation
        
        return NoContent();
    }
    /// <summary>
    /// Rota para obter os detalhes de um cardápio diário específico.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DailyMenuDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDailyMenuDetails(string id,
        [FromServices] IHandler<DailyMenuDto, GetDailyMenuDetailsQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        
        //Validation

        return Ok(await handler.HandleAsync(new GetDailyMenuDetailsQuery(id), cancellationToken));
    }

    /// <summary>
    /// Rota para listar os cardápios diários com base em critérios de pesquisa.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="query"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<DailyMenuDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListDailyMenus(string userId,
        [FromQuery] ListDailyMenuQuery query,
        [FromServices] IHandler<IEnumerable<DailyMenuDto>, ListDailyMenuQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        query.SetUserId(userId);
        //Validation

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}