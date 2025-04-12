namespace SocialService.Connections.Search;

public class AzureSearchProfileDto(string profileId, string name)
{
    public string ProfileId { get; set; } = profileId;
    public string Name { get; set; } = name;
}