using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.CommentOnPost;

/// <summary>
///     Validador para o comando de comentar em um post.
/// </summary>
public class CommentOnPostCommandValidator : AbstractValidator<CommentOnPostCommand>
{
    /// <summary>
    ///     Validações para o comando de comentar em um post.
    /// </summary>
    /// <param name="repository"></param>
    public CommentOnPostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");

        RuleFor(x => x.Content)
            .MaximumLength(1000)
            .WithMessage("Content must be less than 1000 characters.");
    }
}