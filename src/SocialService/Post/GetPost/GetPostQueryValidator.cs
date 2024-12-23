using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.GetPost;

/// <summary>
/// Validador para a query de busca de post.
/// </summary>
public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    /// <summary>
    /// Validações para a query de busca de post.
    /// </summary>
    /// <param name="repository"></param>
    public GetPostQueryValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
    }
}