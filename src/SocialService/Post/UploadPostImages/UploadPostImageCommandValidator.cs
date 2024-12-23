using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.UploadPostImages;

/// <summary>
/// Validador para o comando de upload de imagem de post.
/// </summary>
public class UploadPostImageCommandValidator : AbstractValidator<UploadPostImageCommand>
{
    /// <summary>
    /// Validações para o comando de upload de imagem de post.
    /// </summary>
    /// <param name="repository"></param>
    public UploadPostImageCommandValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
    }
}