using SharedKernel.Enums;

namespace SharedKernel.Dtos;

/// <summary>
/// Evento de notificação
/// </summary>
public class NotificationDto
{
    /// <summary>
    /// Id do destinatário da notificação
    /// </summary>
    public string RecipientId { get; set; }
    
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