using ChatService.Chat.Common.Enum;

namespace ChatService.Chat.Common.Event;

/// <summary>
/// Evento de notificação
/// </summary>
public class NotificationEvent
{
    /// <summary>
    /// Id do destinatário da notificação
    /// </summary>
    public Guid RecipientId { get; set; }
    
    /// <summary>
    /// Enum para tipos de notificações
    /// </summary>
    public ENotificationTopic Topic { get; set; }
    
    /// <summary>
    /// Breve descrição da notificação
    /// </summary>
    public string Content { get; set; } 
    
    /// <summary>
    /// Dados adicionais relacionados à notificação
    /// </summary>
    public object Metadata { get; set; }
}