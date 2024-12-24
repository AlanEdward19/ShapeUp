using FluentValidation;
using SocialService.Profile.Common.Repository;

namespace SocialService.Friends.FriendRequest.SendFriendRequest;

/// <summary>
/// Validador para o comando de enviar solicitação de amizade.
/// </summary>
public class SendFriendRequestCommandValidator : AbstractValidator<SendFriendRequestCommand>
{
    /// <summary>
    /// Validações para o comando de enviar solicitação de amizade.
    /// </summary>
    /// <param name="repository"></param>
    public SendFriendRequestCommandValidator(IProfileGraphRepository repository)
    {
        RuleFor(x => x.FriendId)
            .MustAsync(async (profileId, cancellationToken) => await repository.ProfileExistsAsync(profileId))
            .WithMessage("FriendId: '{PropertyValue}' doesn't exist.");
    }
}