using BDS.DataPack.SharedKernel.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedKernel.Dtos;
using SharedKernel.ValueObjects;
using SharedKernel.Wrappers;

namespace SharedKernel.Providers;

public class GrpcProvider : IGrpcProvider
{
    private readonly MicroserviceGrpcValueObject<NotificationService.NotificationServiceClient> _notificationService;
    private readonly ILogger<GrpcProvider> _logger;
    
    public GrpcProvider(IConfiguration config, ILogger<GrpcProvider> logger)
    {
        string daprPort = config["DAPR_GRPC_PORT"]!;
        string notificationServiceAppId = config.GetSection("DaprConfig")["NOTIFICATION_SERVICE_APP_ID"]!;

        _notificationService = GrpcWrapper<NotificationService.NotificationServiceClient>.GetClientAndMetadataByGprc(
            notificationServiceAppId,
            channel => new NotificationService.NotificationServiceClient(channel), daprPort);

        _logger = logger;
    }
    
    public async Task<bool> SendNotification(NotificationDto notification, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();
        
        _logger.LogInformation("Sending notification to gRPC service for profile {ProfileId}", notification.RecipientId);
        
        var result = await _notificationService.Client.sendNotificationAsync(new NotificationParams
        {
            RecipientId = notification.RecipientId,
            Topic = (int)notification.Topic,
            Content = notification.Content,
            Metadata = notification.Metadata.ToString()
        }, cancellationToken: cancellationToken);

        return result.Success;
    }
}