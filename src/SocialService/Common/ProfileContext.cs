namespace SocialService.Common;

public static class ProfileContext
{
    private static readonly AsyncLocal<Guid> _profileId = new();

    public static Guid ProfileId
    {
        get => _profileId.Value;
        set => _profileId.Value = value;
    }
}