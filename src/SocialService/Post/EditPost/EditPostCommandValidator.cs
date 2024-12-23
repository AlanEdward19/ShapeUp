using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.EditPost;

/// <summary>
/// Validador para o comando de edição de post.
/// </summary>
public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
{
    /// <summary>
    /// Validações para o comando de edição de post.
    /// </summary>
    /// <param name="repository"></param>
    public EditPostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
        
        RuleFor(x => x.Content)
            .MaximumLength(1000)
            .WithMessage("Content must be less than 1000 characters.");
        
        RuleFor(x => x.Visibility)
            .IsInEnum()
            .WithMessage("Visibility: '{PropertyValue}' is not a valid value.");
    }
}