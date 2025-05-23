﻿using SharedKernel.Filters;
using SharedKernel.Utils;
using SocialService.Common.ValueObjects;
using SocialService.Post.Comment.CommentOnPost;
using SocialService.Post.Comment.DeleteCommentOnPost;
using SocialService.Post.Comment.EditCommentOnPost;
using SocialService.Post.Comment.GetPostComments;
using SocialService.Post.Common.Repository;
using SocialService.Post.CreatePost;
using SocialService.Post.DeletePost;
using SocialService.Post.EditPost;
using SocialService.Post.GetPost;
using SocialService.Post.GetPostsByProfileId;
using SocialService.Post.React;
using SocialService.Post.React.DeleteReactionFromPost;
using SocialService.Post.React.GetReactionsOnPost;
using SocialService.Post.React.ReactToPost;
using SocialService.Post.UploadPostImages;
using SocialService.Profile.Common.Repository;

namespace SocialService.Post;

/// <summary>
///     Controller responsavel por gerenciar funções relacionadas a posts, comentários e reações.
/// </summary>
/// <param name="repository"></param>
[ApiVersion("1.0")]
[ApiController]
[TokenValidatorFilter]
[Route("v{version:apiVersion}/[Controller]")]
public class PostController(IPostGraphRepository repository) : ControllerBase
{
    #region Post

    /// <summary>
    ///     Rota para pegar as informações de um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}/getPost")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
    public async Task<IActionResult> GetPost([FromServices] IHandler<PostDto, GetPostQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        GetPostQuery query = new();
        query.SetPostId(id);

        GetPostQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rota para pegar informações dos posts feitos por um perfil
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="profileGraphRepository"></param>
    /// <param name="queryParameters"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("/v{version:apiVersion}/Profile/{id}/getPosts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PostDto>))]
    public async Task<IActionResult> GetPostsByProfileId(
        [FromServices] IHandler<IEnumerable<PostDto>, GetPostsByProfileIdQuery> handler,
        [FromServices] IProfileGraphRepository profileGraphRepository,
        [FromQuery] BaseQueryParametersValueObject queryParameters,
        string id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        GetPostsByProfileIdQuery query = new(queryParameters.Page, queryParameters.Rows);
        query.SetProfileId(id);

        GetPostsByProfileIdQueryValidator validator = new(profileGraphRepository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rota para criar um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("createPost")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostDto))]
    public async Task<IActionResult> CreatePost([FromServices] IHandler<PostDto, CreatePostCommand> handler,
        [FromBody] CreatePostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        CreatePostCommandValidator validator = new();
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
    }

    /// <summary>
    ///     Rota para fazer upload de imagens em um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="files"></param>
    /// <param name="filesToKeep"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}/uploadPostImages")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UploadPostImages([FromServices] IHandler<bool, UploadPostImageCommand> handler,
        Guid id, [FromForm] List<IFormFile> files, [FromForm] List<Guid> filesToKeep,
        CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        var command = new UploadPostImageCommand();
        command.SetPostId(id);
        await command.SetImages(files, cancellationToken);
        command.SetFilesToKeep(filesToKeep);

        UploadPostImageCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Rota para deletar um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}/deletePost")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePost([FromServices] IHandler<bool, DeletePostCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        DeletePostCommand command = new();
        command.SetPostId(id);

        DeletePostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Rota para editar um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}/editPost")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostDto))]
    public async Task<IActionResult> EditPost([FromServices] IHandler<PostDto, EditPostCommand> handler,
        Guid id, [FromBody] EditPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetPostId(id);

        EditPostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        return Created(HttpContext.Request.Path, await handler.HandleAsync(command, cancellationToken));
    }

    #endregion

    #region Comment

    /// <summary>
    ///     Rota para comentar em um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:guid}/commentOnPost")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CommentOnPost([FromServices] IHandler<bool, CommentOnPostCommand> handler,
        Guid id, [FromBody] CommentOnPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetPostId(id);

        CommentOnPostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return Created();
    }

    /// <summary>
    ///     Rota para pegar os comentários de um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}/getComments")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Comment.Comment>))]
    public async Task<IActionResult> GetPostComments(
        [FromServices] IHandler<IEnumerable<Comment.Comment>, GetPostCommentsQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetPostCommentsQuery query = new();
        query.SetPostId(id);

        GetPostCommentsQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rota para editar um comentário em um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}/editComment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditCommentOnPost([FromServices] IHandler<bool, EditCommentOnPostCommand> handler,
        Guid id, [FromBody] EditCommentOnPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetCommentId(id);

        EditCommentOnPostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Rota para deletar um comentário em um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}/deleteComment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCommentOnPost(
        [FromServices] IHandler<bool, DeleteCommentOnPostCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        DeleteCommentOnPostCommand command = new();
        command.SetCommentId(id);

        DeleteCommentOnPostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    #endregion

    #region Reaction

    /// <summary>
    ///     Rotas para reagir a um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}/react")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ReactToPost([FromServices] IHandler<bool, ReactToPostCommand> handler,
        Guid id, [FromBody] ReactToPostCommand command, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();
        command.SetPostId(id);

        ReactToPostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Rota para pegar as reações de um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}/getReactions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Reaction>))]
    public async Task<IActionResult> GetReactionsOnPost(
        [FromServices] IHandler<IEnumerable<Reaction>, GetReactionsOnPostQuery> handler,
        Guid id, CancellationToken cancellationToken)
    {
        GetReactionsOnPostQuery query = new();
        query.SetPostId(id);

        GetReactionsOnPostQueryValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(query, cancellationToken);

        return Ok(await handler.HandleAsync(query, cancellationToken));
    }

    /// <summary>
    ///     Rota para deletar uma reação de um post
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}/deleteReaction")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteReactionFromPost(
        [FromServices] IHandler<bool, DeleteReactionFromPostCommand> handler,
        Guid id, CancellationToken cancellationToken)
    {
        ProfileContext.ProfileId = User.GetObjectId();

        DeleteReactionFromPostCommand command = new();
        command.SetPostId(id);

        DeleteReactionFromPostCommandValidator validator = new(repository);
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        await handler.HandleAsync(command, cancellationToken);
        return NoContent();
    }

    #endregion
}