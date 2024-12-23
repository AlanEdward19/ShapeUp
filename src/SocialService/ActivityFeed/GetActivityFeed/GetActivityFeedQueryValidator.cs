using FluentValidation;

namespace SocialService.ActivityFeed.GetActivityFeed;

/// <summary>
/// Validador para a query de obter o feed de atividades.
/// </summary>
public class GetActivityFeedQueryValidator : AbstractValidator<GetActivityFeedQuery>
{
    /// <summary>
    /// Validações para a query de obter o feed de atividades.
    /// </summary>
    public GetActivityFeedQueryValidator()
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