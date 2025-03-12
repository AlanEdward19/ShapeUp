using SocialService.Common.ValueObjects;
using SocialService.Profile.Common.Repository;
using SocialService.Profile.CreateProfile;
using SocialService.Profile.DeleteProfile;
using SocialService.Profile.EditProfile;
using SocialService.Profile.GetProfilePictures;
using SocialService.Profile.SearchProfileByName;
using SocialService.Profile.UploadProfilePicture;
using SocialService.Profile.ViewProfile;
using SocialService.Profile.ViewProfileSimplified;

namespace SocialService.Profile;

/// <summary>
///     Controller responsavel por gerenciar funções relacionadas ao perfil do usuario
/// </summary>
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("v{version:apiVersion}/[Controller]")]
public class ProfileController(IProfileGraphRepository repository) : ControllerBase
{
    /// <summary>
    ///     Rota para visualizar um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="viewProfilehandler"></param>
    /// <param name="createProfileHandler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("viewProfile/{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileDto))]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProfileDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewProfile(Guid profileId,
        [FromServices] IHandler<ProfileDto?, ViewProfileQuery> viewProfilehandler,
        [FromServices] IHandler<ProfileDto, CreateProfileCommand> createProfileHandler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        ViewProfileQuery query = new(profileId);

        ProfileDto? profile = await viewProfilehandler.HandleAsync(query, cancellationToken);

        if (profile is null && profileId == ProfileContext.ProfileId)
        {
            CreateProfileCommand command = new();

            command.SetEmail(User.GetEmail());
            command.SetFirstName(User.GetFirstName());
            command.SetLastName(User.GetLastName());
            command.SetCountry(User.GetCountry());
            command.SetPostalCode(User.GetPostalCode());
            command.SetDisplayName(User.GetDisplayName());

            CreateProfileCommandValidator createProfileValidator = new();
            await createProfileValidator.ValidateAndThrowAsync(command, cancellationToken);

            profile = await createProfileHandler.HandleAsync(command, cancellationToken);

            return Created(HttpContext.Request.Path, profile);
        }

        return Ok(profile);
    }

    /// <summary>
    ///     Rota para visualizar um perfil simplificado
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("viewProfile/{profileId:guid}/simplified")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileSimplifiedDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewProfileSimplified(Guid profileId,
        [FromServices] IHandler<ProfileSimplifiedDto, ViewProfileSimplifiedQuery> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        ViewProfileSimplifiedQuery query = new(profileId);

        ViewProfileSimplifiedQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    /// Rota para editar um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("editProfile")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileDto))]
    public async Task<IActionResult> EditProfile([FromServices] IHandler<ProfileDto, EditProfileCommand> handler,
        [FromBody] EditProfileCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        EditProfileCommandValidator validator = new();
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    /// Rota para deletar um perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="handler"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("deleteProfile/{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteProfile(Guid profileId,
        [FromServices] IHandler<bool, DeleteProfileCommand> handler,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        DeleteProfileCommand command = new(profileId);

        DeleteProfileCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Rota para fazer upload de uma foto de perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="image"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("uploadProfilePicture")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UploadProfilePicture(
        [FromServices] IHandler<bool, UploadProfilePictureCommand> handler,
        IFormFile image, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        UploadProfilePictureCommand command = new();
        await command.SetImage(image, image.FileName, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Rota para obter fotos de perfil.
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="handler"></param>
    /// <param name="queryParameters"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getProfilePictures/{profileId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProfilePicture>))]
    public async Task<IActionResult> GetProfilePictures(Guid profileId,
        [FromServices] IHandler<IEnumerable<ProfilePicture>, GetProfilePicturesQuery> handler,
        [FromQuery] BaseQueryParametersValueObject queryParameters, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        GetProfilePicturesQuery query = new(profileId, queryParameters.Page, queryParameters.Rows);

        GetProfilePicturesQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    /// Rota para buscar um perfil pelo nome
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("searchProfileByName")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProfileSimplifiedDto>))]
    public async Task<IActionResult> SearchProfileByName(
        [FromServices] IHandler<IEnumerable<ProfileSimplifiedDto>, SearchProfileByNameQuery> handler,
        [FromQuery] SearchProfileByNameQuery query, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
}