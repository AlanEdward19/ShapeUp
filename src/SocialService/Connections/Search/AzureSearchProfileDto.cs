namespace SocialService.Connections.Search;

public class AzureSearchProfileDto(Guid profileId, string name)
{
    public Guid ProfileId { get; set; } = profileId;
    public string Name { get; set; } = name;
}