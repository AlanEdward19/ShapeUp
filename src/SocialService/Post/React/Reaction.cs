using SocialService.Post.Common.Enums;
using SocialService.Post.React.ReactToPost;

namespace SocialService.Post.React;

/// <summary>
///     Classe que representa uma reação
/// </summary>
public class Reaction : GraphEntity
{
    /// <summary>
    ///     Construtor padrão
    /// </summary>
    public Reaction()
    {
    }

    /// <summary>
    ///     Construtor para criar uma nova reação.
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="command"></param>
    public Reaction(Guid profileId, ReactToPostCommand command)
    {
        if (profileId == Guid.Empty)
            throw new ArgumentException("ProfileId cannot be empty.");

        if (Enum.IsDefined(typeof(EReactionType), command.ReactionType) == false)
            throw new ArgumentException("ReactionType value is invalid.");

        Id = Guid.NewGuid();
        ProfileId = profileId;
        PostId = command.PostId;
        ReactionType = command.ReactionType.ToString();
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     Id do perfil que reagiu
    /// </summary>
    public Guid ProfileId { get; private set; }

    /// <summary>
    ///     Data de criação da reação
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    ///     Tipo da reação
    /// </summary>
    public string ReactionType { get; private set; }

    /// <summary>
    ///     Id do post
    /// </summary>
    public Guid PostId { get; private set; }

    /// <summary>
    ///     Método para mapear os dados do neo4j para a entidade
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        ProfileId = Guid.Parse(result["profileId"].ToString());
        ReactionType = result["type"].ToString();
        PostId = Guid.Parse(result["postId"].ToString());
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());

        base.MapToEntityFromNeo4j(result);
    }

    /// <summary>
    ///     Método para atualizar o tipo de reação.
    /// </summary>
    /// <param name="reactionType"></param>
    public void UpdateReactionType(string reactionType)
    {
        if (string.IsNullOrWhiteSpace(reactionType))
            throw new ArgumentException("ReactionType cannot be empty.");

        if (ReactionType != reactionType)
            ReactionType = reactionType;
    }
}