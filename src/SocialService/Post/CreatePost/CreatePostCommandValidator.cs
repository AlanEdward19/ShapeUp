using FluentValidation;

namespace SocialService.Post.CreatePost;

/// <summary>
/// Validador para o comando de criação de post.
/// </summary>
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    /// <summary>
    /// Validações para o comando de criação de post.
    /// </summary>
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Content)
            .MaximumLength(1000)
            .WithMessage("Content must be less than 1000 characters.");

        RuleFor(x => x.Visibility)
            .IsInEnum()
            .WithMessage("Visibility: '{PropertyValue}' is not a valid value.");
    }
}