namespace SocialService.Recommendation.GetFriendRecommendations;

/// <summary>
///     Validador para a query de obter recomendações de amigos.
/// </summary>
public class GetFriendRecommendationQueryValidator : AbstractValidator<GetFriendRecommendationQuery>
{
    /// <summary>
    ///     Validações para a query de obter recomendações de amigos.
    /// </summary>
    public GetFriendRecommendationQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page must be greater than or equal to 1.");

        RuleFor(x => x.Rows)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Rows must be greater than or equal to 1.");

        RuleFor(x => x.Rows)
            .LessThanOrEqualTo(1000)
            .WithMessage("Rows must be less than or equal to 1000.");
    }
}