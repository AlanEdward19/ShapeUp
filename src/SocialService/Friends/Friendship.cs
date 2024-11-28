using MongoDB.Bson.Serialization.Attributes;
using SocialService.Common.Entities;
using SocialService.Friends.Common.Enums;

namespace SocialService.Friends;

/// <summary>
/// Classe que representa uma amizade entre dois perfis
/// </summary>
public class Friendship : GraphEntity
{
    public Friendship(Guid profileAId, Guid profileBId) : base("Friendship")
    {
        ProfileAId = profileAId.ToString();
        ProfileBId = profileBId.ToString();
    }

    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        Id = result["id"].ToString();
        ProfileAId = result["profileAId"].ToString();
        ProfileBId = result["profileBId"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
    }

    public string ProfileAId { get; set; } // One of the profiles involved in the friendship
    public string ProfileBId { get; set; } // The other profile involved in the friendship
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}