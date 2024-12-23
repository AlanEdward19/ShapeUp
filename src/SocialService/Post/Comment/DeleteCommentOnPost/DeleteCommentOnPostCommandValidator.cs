using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.DeleteCommentOnPost;

/// <summary>
/// Validador para o comando de deletar um comentário em um post.
/// </summary>
public class DeleteCommentOnPostCommandValidator : AbstractValidator<DeleteCommentOnPostCommand>
{
    /// <summary>
    /// Validações para o comando de deletar um comentário em um post.
    /// </summary>
    /// <param name="repository"></param>
    public DeleteCommentOnPostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.CommentId)
            .MustAsync(async (commentId, cancellationToken) => await repository.CommentExistsAsync(commentId))
            .WithMessage("CommentId: '{PropertyValue}' doesn't exist.");
    }
}