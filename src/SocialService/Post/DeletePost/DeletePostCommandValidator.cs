using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.DeletePost;

/// <summary>
/// Validador para o comando de deletar um post.
/// </summary>
public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    /// <summary>
    /// Validações para o comando de deletar um post.
    /// </summary>
    /// <param name="repository"></param>
    public DeletePostCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
    }
}