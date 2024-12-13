using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Utils;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;

namespace SocialService.Profile;

/// <summary>
///     Controller responsavel por gerenciar o perfil do usuario
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class ProfileController : ControllerBase
{
    /// <summary>
    ///     Rota para criar um perfil
    /// </summary>
    /// <returns></returns>
    [HttpPost("createProfile")]
    public async Task<IActionResult> CreateProfile(
        [FromServices] IHandler<ProfileAggregate, CreateProfileCommand> handler,
        [FromBody] CreateProfileCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        command.SetEmail(User.GetEmail());
        command.SetFirstName(User.GetFirstName());
        command.SetLastName(User.GetLastName());
        command.SetCity(User.GetCity());
        command.SetState(User.GetState());
        command.SetCountry(User.GetCountry());

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    ///     Rota para visualizar um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("viewProfile/{profileId:guid}")]
    public async Task<IActionResult> ViewProfile(Guid profileId,
        [FromServices] IHandler<ProfileAggregate, ViewProfileQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new ViewProfileQuery(profileId), cancellationToken));
    }

    /// <summary>
    ///     Rota para editar um perfil
    /// </summary>
    /// <returns></returns>
    [HttpPatch("editProfile")]
    public async Task<IActionResult> EditProfile([FromServices] IHandler<ProfileAggregate, EditProfileCommand> handler,
        [FromBody] EditProfileCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    ///     Rota para deletar um perfil
    /// </summary>
    /// <returns></returns>
    [HttpDelete("deleteProfile/{profileId:guid}")]
    public async Task<IActionResult> DeleteProfile(Guid profileId,
        [FromServices] IHandler<bool, DeleteProfileCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(new DeleteProfileCommand(profileId), cancellationToken));
    }

    /// <summary>
    ///     Rota para fazer upload de uma foto de perfil
    /// </summary>
    /// <returns></returns>
    [HttpPut("uploadProfilePicture")]
    public async Task<IActionResult> UploadProfilePicture(
        [FromServices] IHandler<bool, UploadProfilePictureCommand> handler,
        IFormFile image, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        UploadProfilePictureCommand command = new();
        await command.SetImage(image, image.FileName, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
}