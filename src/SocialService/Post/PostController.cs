using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Utils;
using SocialService.Post.CreatePost;
using SocialService.Post.UploadPostImages;

namespace SocialService.Post;

[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PostController : ControllerBase
{
    [HttpPost("createPost")]
    public async Task<IActionResult> CreatePost([FromServices] IHandler<Post, CreatePostCommand> handler,
        [FromBody] CreatePostCommand command, [FromForm] List<IFormFile> files, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpPut("{id:guid}/uploadPostImages")]
    public async Task<IActionResult> UploadPostImages([FromServices] IHandler<bool, UploadPostImageCommand> handler,
        Guid id,[FromForm] List<IFormFile> files, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        var command = new UploadPostImageCommand();
        
        command.SetPostId(id);
        await command.SetImages(files, cancellationToken);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
}