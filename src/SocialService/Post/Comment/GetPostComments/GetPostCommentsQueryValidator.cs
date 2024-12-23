using FluentValidation;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.Comment.GetPostComments;

/// <summary>
/// Validador para a query de obter os comentários de um post
/// </summary>
public class GetPostCommentsQueryValidator : AbstractValidator<GetPostCommentsQuery>
{
    /// <summary>
    /// Validações para a query de obter os comentários de um post
    /// </summary>
    /// <param name="repository"></param>
    public GetPostCommentsQueryValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
    }
}