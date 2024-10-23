using SocialService.Profile.Common.Enums;

namespace SocialService.Profile;

public class ProfileAggregate(Profile profile)
{
    public Guid ObjectId { get; private set; } = profile.ObjectId;
    public string Email { get; private set; } = profile.Email;
    public string? ImageUrl { get; private set; } = profile.ImageUrl;
    public string? Bio { get; private set; } = profile.Bio;
    public DateTime BirthDate { get; private set; } = profile.BirthDate;
    public EGender Gender { get; private set; } = profile.Gender;

    public void UpdateImage(string? imageUrl)
    {
        if (!string.IsNullOrWhiteSpace(imageUrl) &&
            (ImageUrl == null || !ImageUrl.Equals(imageUrl, StringComparison.InvariantCultureIgnoreCase)))
            ImageUrl = imageUrl;
    }

    public void UpdateEmail(string? email)
    {
        if (!string.IsNullOrWhiteSpace(email) && !Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
            Email = email;
    }

    public void UpdateBio(string? bio)
    {
        if (!string.IsNullOrWhiteSpace(bio) &&
            (Bio == null || !Bio.Equals(bio, StringComparison.InvariantCultureIgnoreCase)))
            Bio = bio;
    }

    public void UpdateGender(EGender? gender)
    {
        if (gender != null)
            Gender = gender.Value;
    }

    public void UpdateBirthDate(DateTime? birthDate)
    {
        if (birthDate != null)
            BirthDate = birthDate.Value;
    }
}