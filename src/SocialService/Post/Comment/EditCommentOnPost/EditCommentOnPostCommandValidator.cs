using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.EditCommentOnPost;

/// <summary>
///     Validador para o comando de editar um comentário em um post.
/// </summary>
public class EditCommentOnPostCommandValidator : AbstractValidator<EditCommentOnPostCommand>
{
    /// <summary>
    ///     Validações para o comando de editar um comentário em um post.
    /// </summary>
    /// <param name="repository"></param>
    public EditCommentOnPostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.CommentId)
            .MustAsync(async (commentId, cancellationToken) => await repository.CommentExistsAsync(commentId))
            .WithMessage("CommentId: '{PropertyValue}' doesn't exist.");

        RuleFor(x => x.Content)
            .MaximumLength(1000)
            .WithMessage("Content must be less than 1000 characters.");
    }
}