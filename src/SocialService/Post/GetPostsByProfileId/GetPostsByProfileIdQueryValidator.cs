﻿using SocialService.Profile.Common.Repository;

namespace SocialService.Post.GetPostsByProfileId;

/// <summary>
///     Validador para a query de busca de post.
/// </summary>
public class GetPostsByProfileIdQueryValidator : AbstractValidator<GetPostsByProfileIdQuery>
{
    /// <summary>
    ///     Validações para a query de busca de post.
    /// </summary>
    /// <param name="repository"></param>
    public GetPostsByProfileIdQueryValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.ProfileId)
            .MustAsync(async (profileId, _) => await repository.ProfileExistsAsync(profileId))
            .WithMessage("ProfileId: '{PropertyValue}' doesn't exist.");
        
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