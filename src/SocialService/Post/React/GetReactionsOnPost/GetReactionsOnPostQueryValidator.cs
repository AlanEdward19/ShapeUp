using SocialService.Post.Common.Repository;

namespace SocialService.Post.React.GetReactionsOnPost;

/// <summary>
///     Validador para a query de buscar reações em um post.
/// </summary>
public class GetReactionsOnPostQueryValidator : AbstractValidator<GetReactionsOnPostQuery>
{
    /// <summary>
    ///     Validações para a query de buscar reações em um post.
    /// </summary>
    /// <param name="repository"></param>
    public GetReactionsOnPostQueryValidator(IPostGraphRepository repository)
    {
        RuleFor(x => x.PostId)
            .MustAsync(async (postId, cancellationToken) => await repository.PostExistsAsync(postId))
            .WithMessage("PostId: '{PropertyValue}' doesn't exist.");
    }
}