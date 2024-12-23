using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.React.ReactToPost;

/// <summary>
/// Validador para o comando de reação a um post.
/// </summary>
public class ReactToPostCommandValidator : AbstractValidator<ReactToPostCommand>
{
    /// <summary>
    /// Validações para o comando de reação a um post.
    /// </summary>
    /// <param name="repository"></param>
    public ReactToPostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
        
        RuleFor(x => x.ReactionType)
            .IsInEnum()
            .WithMessage("ReactionType: '{PropertyValue}' is not a valid value.");
    }
}