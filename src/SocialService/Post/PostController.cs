using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Common.Utils;
using SocialService.Post.CommentsOnPost;
using SocialService.Post.CreatePost;
using SocialService.Post.DeleteCommentOnPost;
using SocialService.Post.DeletePost;
using SocialService.Post.DeleteReactionFromPost;
using SocialService.Post.EditCommentOnPost;
using SocialService.Post.GetPostComments;
using SocialService.Post.GetPostInformations;
using SocialService.Post.GetReactionsOnPost;
using SocialService.Post.LikePost;
using SocialService.Post.UploadPostImages;

namespace SocialService.Post;

[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]/v{version:apiVersion}")]
public class PostController : ControllerBase
{
    #region Post

    [HttpGet("{id:guid}/getPost")]
    public async Task<IActionResult> GetPost([FromServices] IHandler<Post, GetPostQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetPostQuery query = new();
        query.SetPostId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    
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
    
    [HttpDelete("{id:guid}/deletePost")]
    public async Task<IActionResult> DeletePost([FromServices] IHandler<bool, DeletePostCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        DeletePostCommand command = new();
        command.SetPostId(id);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpPatch("{id:guid}/editPost")]
    public async Task<IActionResult> EditPost([FromServices] IHandler<Post, EditPostCommand> handler,
        Guid id, [FromBody] EditPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        command.SetPostId(id);

        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    #endregion

    #region Comment

    [HttpPost("{id:guid}/commentOnPost")]
    public async Task<IActionResult> CommentOnPost([FromServices] IHandler<bool, CommentOnPostCommand> handler,
        Guid id, [FromBody] CommentOnPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        command.SetPostId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpGet("{id:guid}/getComments")]
    public async Task<IActionResult> GetPostComments([FromServices] IHandler<IEnumerable<Comment>, GetPostCommentsQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetPostCommentsQuery query = new();
        query.SetPostId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpPut("{id:guid}/editComment")]
    public async Task<IActionResult> EditCommentOnPost([FromServices] IHandler<bool, EditCommentOnPostCommand> handler,
        Guid id, [FromBody] EditCommentOnPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        command.SetCommentId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpDelete("{id:guid}/deleteComment")]
    public async Task<IActionResult> DeleteCommentOnPost([FromServices] IHandler<bool, DeleteCommentOnPostCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        DeleteCommentOnPostCommand command = new();
        command.SetCommentId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    #endregion

    #region Reaction

    [HttpPut("{id:guid}/react")]
    public async Task<IActionResult> ReactToPost([FromServices] IHandler<bool, ReactToPostCommand> handler,
        Guid id, [FromBody] ReactToPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        command.SetPostId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }
    
    [HttpGet("{id:guid}/getReactions")]
    public async Task<IActionResult> GetReactionsOnPost([FromServices] IHandler<IEnumerable<Reaction>, GetReactionsOnPostQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetReactionsOnPostQuery query = new();
        query.SetPostId(id);
        
        return Ok(await handler.HandleAsync(query, cancellationToken));
    }
    
    [HttpDelete("{id:guid}/deleteReaction")]
    public async Task<IActionResult> DeleteReactionFromPost([FromServices] IHandler<bool, DeleteReactionFromPostCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = Guid.Parse(User.GetObjectId());
        
        DeleteReactionFromPostCommand command = new();
        command.SetPostId(id);
        
        return Ok(await handler.HandleAsync(command, cancellationToken));
    }

    #endregion
}