using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Utils;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.GetProfilePictures;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;

namespace SocialService.Profile;

/// <summary>
///     Controller responsavel por gerenciar funções relacionadas ao perfil do usuario
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class ProfileController(IProfileGraphRepository repository) : ControllerBase
{
    /// <summary>
    ///     Rota para criar um perfil
    /// </summary>
    /// <returns></returns>
    [HttpPost("createProfile")]
    public async Task<IActionResult> CreateProfile(
        [FromServices] IHandler<ProfileDto, CreateProfileCommand> handler,
        [FromBody] CreateProfileCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        command.SetEmail(User.GetEmail());
        command.SetFirstName(User.GetFirstName());
        command.SetLastName(User.GetLastName());
        command.SetCity(User.GetCity());
        command.SetState(User.GetState());
        command.SetCountry(User.GetCountry());
        
        CreateProfileCommandValidator validator = new();
        await validator.ValidateAndThrowAsync(command, cancellationToken);

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
        [FromServices] IHandler<ProfileDto, ViewProfileQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        ViewProfileQuery query = new(profileId);
        
        ViewProfileQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rota para editar um perfil
    /// </summary>
    /// <returns></returns>
    [HttpPatch("editProfile")]
    public async Task<IActionResult> EditProfile([FromServices] IHandler<ProfileDto, EditProfileCommand> handler,
        [FromBody] EditProfileCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        EditProfileCommandValidator validator = new();
        await validator.ValidateAndThrowAsync(command, cancellationToken);

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
        
        DeleteProfileCommand command = new(profileId);
        
        DeleteProfileCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
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
    
    /// <summary>
    /// Rota para obter fotos de perfil.
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getProfilePictures/{profileId:guid}")]
    public async Task<IActionResult> GetProfilePictures(Guid profileId,
        [FromQuery] int? page, [FromQuery] int? rows,
        [FromServices] IHandler<IEnumerable<ProfilePicture>, GetProfilePicturesQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        GetProfilePicturesQuery query = new(profileId, page, rows);
        
        GetProfilePicturesQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}