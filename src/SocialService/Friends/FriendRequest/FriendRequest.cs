namespace SocialService.Friends.FriendRequest;

/// <summary>
///     Classe que representa uma solicitação de amizade
/// </summary>
public class FriendRequest : GraphEntity
{
    /// <summary>
    ///     Perfil que enviou a solicitação
    /// </summary>
    public string SenderProfileId { get; private set; }

    /// <summary>
    ///     Perfil que recebeu a solicitação
    /// </summary>
    public string ReceiverProfileId { get; private set; }

    /// <summary>
    ///     Mensagem enviada com a solicitação
    /// </summary>
    public string Message { get; set; } = "";

    /// <summary>
    ///     Data de criação da solicitação
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     Método para mapear os dados da solicitação de amizade de um dicionário
    /// </summary>
    /// <param name="result"></param>
    public override void MapToEntityFromNeo4j(Dictionary<string, object> result)
    {
        SenderProfileId = result["senderProfileId"].ToString();
        ReceiverProfileId = result["receiverProfileId"].ToString();
        CreatedAt = DateTime.Parse(result["createdAt"].ToString());
        Message = result["message"].ToString();

        base.MapToEntityFromNeo4j(result);
    }
}