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
    /// Título da notificação
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Breve descrição da notificação
    /// </summary>
    public string Body { get; set; }
    
    /// <summary>
    /// Dados adicionais relacionados à notificação
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; }
}