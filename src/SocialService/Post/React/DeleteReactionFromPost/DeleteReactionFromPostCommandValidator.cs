using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.React.DeleteReactionFromPost;

/// <summary>
/// Validador para o comando de deletar uma reação de um post
/// </summary>
public class DeleteReactionFromPostCommandValidator : AbstractValidator<DeleteReactionFromPostCommand>
{
    /// <summary>
    /// Validações para o comando de deletar uma reação de um post
    /// </summary>
    /// <param name="repository"></param>
    public DeleteReactionFromPostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
    }
}